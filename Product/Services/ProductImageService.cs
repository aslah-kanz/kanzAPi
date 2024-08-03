using KanzApi.Common.Services;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using MapsterMapper;
using KanzApi.Product.Entities;
using KanzApi.Product.Repositories;
using KanzApi.Common.Models.Param;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Services;

public class ProductImageService(IProductImageRepository repository, IMapper mapper, IProductSyncableService productService)
: CrudService<ProductImage, int?>(repository), IProductImageService
{

    private readonly IMapper _mapper = mapper;
    private readonly IProductSyncableService _productService = productService;

    public ProductImageResponse Add(int productId, ProductImageRequest request)
    {
        ProductImage entity = _mapper.Map<ProductImage>(request);

        ProductEntity product = _productService.GetById(productId);
        entity.Product = product;

        entity = Add(entity);

        return _mapper.Map<ProductImageResponse>(entity);
    }

    public ProductImageResponse Edit(int productId, int id, ProductImageRequest request)
    {
        ProductImage entity = GetById(productId, id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<ProductImageResponse>(entity);
    }

    public ProductImageResponse RemoveModelById(int productId, int id)
    {
        ProductImage entity = GetById(productId, id);
        entity = Remove(entity);

        return _mapper.Map<ProductImageResponse>(entity);
    }

    public ProductImage GetById(int productId, int id)
    {
        return FindById(id, ProductImage.QProductIdEquals(productId))
        ?? throw EntityNotFoundException.From(typeof(ProductImage),
        new Dictionary<string, object?> { { "Product ID", productId }, { "ID", id } });
    }

    public ProductImageResponse GetModelById(int productId, int id)
    {
        ProductImage entity = GetById(productId, id);
        return _mapper.Map<ProductImageResponse>(entity);
    }

    public IEnumerable<ProductImageResponse> FindAllModels(int productId, ProductImageSortableParam param)
    {
        IEnumerable<ProductImage> entities = FindAll(ProductImage.QProductIdEquals(productId), Sort.From(param));
        return entities.Select(_mapper.Map<ProductImageResponse>);
    }
}
