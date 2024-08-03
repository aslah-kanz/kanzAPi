using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Configurations.Contexts;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Repositories;
using KanzApi.Utils;
using MapsterMapper;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.Linq.Expressions;

namespace KanzApi.Product.Services;

public class ProductSyncableService(IProductRepository repository, IMapper mapper,
IPrincipalDetailFilterableService principalDetailService, ICategoryService categoryService,
ISearchEngineService<ProductFamilyProductItemMeilisearch> searchEngineService,
IProductAttributeService productAttributeService, IAttributeService attributeService,
CommonDbContext dbContext)
: CrudService<ProductEntity, int?>(repository), IProductSyncableService
{

    private readonly IMapper _mapper = mapper;

    private readonly CommonDbContext _dbContext = dbContext;

    private readonly ICategoryService _categoryService = categoryService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    private readonly IAttributeService _attributeService = attributeService;

    private readonly IProductAttributeService _productAttributeService = productAttributeService;

    private ISearchEngineService<ProductFamilyProductItemMeilisearch> _searchEngineService = searchEngineService;

    public ProductResponse Add(ProductRequest request)
    {
        ProductEntity entity = _mapper.Map<ProductEntity>(request);

        PrincipalDetail principalDetail = _principalDetailService.GetByCurrentPrincipal();
        entity.PrincipalDetailId = principalDetail.Id;

        string slug = ProductUtils.ToUrlSlug(request.Name!.En!);
        bool isSlugDuplicate = CheckSlugAvailability(slug);
        if (isSlugDuplicate) throw new DuplicateProductSlugException(slug);
        entity.Slug = slug;

        if (request.Image == null)
        {
            var defaultThumbnail = _dbContext.Images.FirstOrDefault(x => x.Name == "tempProductThumbnail.webp");
            if (defaultThumbnail != null)
            {
                entity.ImageId = defaultThumbnail.Id;
            }
        }

        if (request.Icon == null)
        {
            var defaultIcon = _dbContext.Images.FirstOrDefault(x => x.Name == "tempProductIcon.webp");
            if (defaultIcon != null)
            {
                entity.IconId = defaultIcon.Id;
            }
        }

		if (request.ProductImages != null)
		{
			foreach (var imageDoc in request.ProductImages)
			{
				entity.Images.Add(new ProductImage()
				{
					ImageId = imageDoc,
					ProductId = entity.Id
				});
			}
		}

        entity = Add(entity);

        if (request.Documents != null)
        {
            var attributeForDocument = _attributeService.FindAll(a => a.NameEn == "Document", null).FirstOrDefault();
            var propertyForDocument = _dbContext.Properties.FirstOrDefault(p => p.Type == EPropertyType.Document);

            if (attributeForDocument != null && propertyForDocument != null && request.Documents.Count > 0)
            {
                var productAttribute = new ProductAttribute
                {
                    AttributeId = attributeForDocument.Id,
                    PropertyId = propertyForDocument.Id,
                    Value1En = string.Join(",", request.Documents),
                    ProductId = entity.Id
                };
                _productAttributeService.Add(productAttribute);
			}
        }

		_searchEngineService.SyncProductsByFamilyCode(entity.FamilyCode!);

        return GetModelById(entity.Id!.Value);
    }

    public ProductResponse ChangeStatus(int id, ProductStatusRequest request)
    {
        ProductEntity entity = GetById(id);
        entity.Status = request.Status;

        entity = Edit(entity);

        _searchEngineService.UpdateDocumentProductStatus(entity.FamilyCode!, id, request.Status!.Value);

        return _mapper.Map<ProductResponse>(entity);
    }

    public ProductResponse Edit(int id, ProductRequest request)
    {
        ProductEntity entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        _searchEngineService.SyncProductsByFamilyCode(entity.FamilyCode!);

        return _mapper.Map<ProductResponse>(entity);
    }

    public ProductResponse RemoveModelById(int id)
    {
        var attributeForDocument = _attributeService.FindAll(a => a.NameEn == "Document", null).FirstOrDefault();
        var propertyForDocument = _dbContext.Properties.FirstOrDefault(p => p.Type == EPropertyType.Document);

        if (attributeForDocument != null && propertyForDocument != null)
        {
            var productDocumentAttributes = _dbContext.ProductAttributes
                .Where(x => x.ProductId == id && x.AttributeId == attributeForDocument.Id && x.PropertyId == propertyForDocument.Id);

            foreach (var productAttribute in productDocumentAttributes)
            {
                _productAttributeService.RemoveById(productAttribute.Id);
            }
        }

        ProductEntity entity = RemoveById(id);

        _searchEngineService.SyncProductsByFamilyCode(entity.FamilyCode!, id);

        return _mapper.Map<ProductResponse>(entity);
    }

    public ProductResponse GetModelById(int id)
    {
        ProductEntity entity = GetById(id);

        var mappedEntity = _mapper.Map<ProductResponse>(entity);

        var attributeForDocument = _attributeService.FindAll(a => a.NameEn == "Document", null).FirstOrDefault();
        var propertyForDocument = _dbContext.Properties.FirstOrDefault(p => p.Type == EPropertyType.Document);

        if (attributeForDocument == null || propertyForDocument == null) return mappedEntity;

        var productDocumentAttributes = _productAttributeService
            .FindAll(ProductAttribute.QProductIdEquals(id), null)
            .FirstOrDefault(x => x.AttributeId == attributeForDocument.Id && x.PropertyId == propertyForDocument.Id);

        if (productDocumentAttributes == null) return mappedEntity;

        var documentIds = productDocumentAttributes.Value1En?.Split(",").Select(long.Parse).ToList();

        if (documentIds == null) return mappedEntity;

        var documents = _dbContext.Documents.Where(d => documentIds.Contains(d.Id!.Value)).ToList();

        mappedEntity.Documents = documents.Select(d => d.Url);

        return mappedEntity;
    }

    public bool CheckSlugAvailability(string slug)
    {
        return _dbContext.Products.Any(x => x.Slug == slug);
    }

    public IEnumerable<OverviewCategoryItem> FindAllOverviewCategoryModels()
    {
        return _categoryService.FindAll(Category.QShowAtHomePageEquals(true), null)
        .Select(category =>
        {
            OverviewCategoryItem categoryModel = _mapper.Map<OverviewCategoryItem>(category);
            Expression<Func<ProductEntity, bool>>? predicate = ProductEntity.QCategoryIdsContains((int)category.Id!);

            categoryModel.Products = FindAll(new Page(0, 10), predicate, new Sort("UpdatedAt", EOrder.Asc)).Content
            .Select(entity => _mapper.Map<OverviewProductResponse>(entity)).ToHashSet();
            return categoryModel;
        });
    }
}
