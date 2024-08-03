using AttributeEntity = KanzApi.Product.Entities.Attribute;
using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Repositories;
using MapsterMapper;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using KanzApi.Configurations.Contexts;

namespace KanzApi.Product.Services;

public class SaleProductFilterableService(IProductRepository repository, IMapper mapper,
ISaleItemSellableService saleItemService, IPrincipalService principalService,
ICategoryService categoryService, IProductAttributeService productAttributeService,
IAttributeService attributeService, CommonDbContext commonDbContext)
: FilterableCrudService<ProductEntity, int?>(repository), ISaleProductFilterableService
{

	private readonly IMapper _mapper = mapper;

	private readonly ISaleItemSellableService _saleItemService = saleItemService;

	private readonly IPrincipalService _principalService = principalService;

	private readonly ICategoryService _categoryService = categoryService;

	private readonly IProductAttributeService _productAttributeService = productAttributeService;

	private readonly IAttributeService _attributeService = attributeService;

	private readonly CommonDbContext _dbContext = commonDbContext;

	private readonly List<string> _relatedFamilyDummies = [
		"DCF961B",
		"DCM200E1",
		"A088",
		"A100",
		"A130",
		"A124",
		"ISO BARS A042",
		"P505",
		"P701"
	];

	public override ProductEntity Add(ProductEntity entity)
	{
		throw new NotSupportedException();
	}

	public override ProductEntity Edit(ProductEntity entity)
	{
		throw new NotSupportedException();
	}

	public override ProductEntity Remove(ProductEntity entity)
	{
		throw new NotSupportedException();
	}

	public IEnumerable<ProductPriceResponse> FindAllPricesById(int id, string? countryCode = null)
	{
		IEnumerable<SaleItem> entities = (countryCode == null)
			? _saleItemService.FindAllByProductId(id)
			: _saleItemService.FindAllByProductIdAndCountry(id, countryCode);
		Dictionary<decimal, ProductPriceResponse> models = [];

		foreach (SaleItem entity in entities)
		{
			decimal price = (decimal)entity.Price!;
			decimal? originalPrice = entity.OriginalPrice;

			models.TryGetValue(price, out ProductPriceResponse? model);
			if (model == null)
			{
				model = new()
				{
					ItemIds = { (long)entity.Id! },
					Price = price,
					OriginalPrice = originalPrice,
					Stock = (int)entity.Stock!
				};
				models[price] = model;
			}
			else
			{
				model.ItemIds.Add((long)entity.Id!);

				if (originalPrice != null
				&& (model.OriginalPrice == null || originalPrice < model.OriginalPrice))
				{
					model.OriginalPrice = originalPrice;
				}

				model.Stock += (int)entity.Stock!;
			}
		}

		return models.Values.OrderBy(e => e.Price);
	}

	private SalePropertyGroupResponse FindPropertyGroupByNameOrCreate(
		AttributeEntity attribute, ISet<SalePropertyGroupResponse> models)
	{
		string name = attribute.GroupEn!;
		SalePropertyGroupResponse? result = null;
		foreach (SalePropertyGroupResponse model in models)
		{
			if (name.Equals(model.Name.En))
			{
				result = model;
				break;
			}
		}

		int order = (int)attribute.GroupOrder!;
		if (result == null)
		{
			result = new()
			{
				Name = new()
				{
					En = name,
					Ar = attribute.GroupAr
				},
				SortOrder = order
			};
			models.Add(result);
		}
		else if (order < result.SortOrder)
		{
			result.SortOrder = order;
		}

		return result;
	}

	private SalePropertyResponse FindPropertyOrCreate(
		ProductAttribute productAttribute, ISet<SalePropertyResponse> models)
	{
		Property property = productAttribute.Property!;
		string name = property.NameEn!;

		SalePropertyResponse? result = null;
		foreach (SalePropertyResponse model in models)
		{
			if (name.Equals(model.Name.En))
			{
				result = model;
				break;
			}
		}

		if (result == null)
		{
			result = new()
			{
				Name = new()
				{
					En = name,
					Ar = property.NameAr
				},
				Type = (EPropertyType)property.Type!,
				Fields = property.ToLocalizedFields(),
				SortOrder = (int)property.SortOrder!
			};
			models.Add(result);
		}

		return result;
	}

	public SaleProductResponse Map(ProductEntity entity)
	{
		SaleProductResponse model = _mapper.Map<SaleProductResponse>(entity);
		model.Images = entity.Images.OrderBy(o => o.SortOrder).Select(e => _mapper.Map<ImageResponse>(e.Image!));
		model.Prices = FindAllPricesById((int)entity.Id!);

		// Get documents by product entity
		var attributeForDocument = _attributeService.FindAll(a => a.NameEn == "Document", null).FirstOrDefault();
		var propertyForDocument = _dbContext.Properties.FirstOrDefault(p => p.Type == EPropertyType.Document);

		if (attributeForDocument != null && propertyForDocument != null)
		{
			var productDocumentAttributes = _productAttributeService
				.FindAll(ProductAttribute.QProductIdEquals(entity.Id!.Value), null)
				.FirstOrDefault(x => x.AttributeId == attributeForDocument.Id && x.PropertyId == propertyForDocument.Id);

			if (productDocumentAttributes != null)
			{
				var documentIds = productDocumentAttributes.Value1En?.Split(",").Select(long.Parse).ToList();

				if (documentIds != null)
				{
					var documents = _dbContext.Documents.Where(d => documentIds.Contains(d.Id!.Value)).ToList();
					model.Documents = documents.Select(x => new DocumentResponse
					{
						Id = x.Id!.Value,
						Name = x.Name!,
						Type = x.Type!,
						Url = x.Url!,
					});
				}
			}
		}

		model.Properties = MapProperties(entity.Attributes.Where(x => x.Attribute?.NameEn != "Document"));

		IEnumerable<ProductEntity> otherEntities = FindAll(ProductEntity.QFamilyCodeEquals(model.FamilyCode!), null);
		model.OtherProducts = otherEntities.OrderBy(o => o.SortOrder).Select(_mapper.Map<ProductFamilyProductItem>);

		model.CategoryTree = entity.Categories.FirstOrDefault()?.LinkedPath().Select(_mapper.Map<ProductCategoryResponse>) ?? [];

		return model;
	}

	private SalePropertyItemResponse Map(ProductAttribute productAttribute, AttributeEntity attribute)
	{
		return new()
		{
			Description = new()
			{
				En = attribute.NameEn,
				Ar = attribute.NameAr
			},
			Value1 = new()
			{
				En = productAttribute.Value1En,
				Ar = productAttribute.Value1Ar
			},
			Unit1 = new()
			{
				En = attribute.Unit1En,
				Ar = attribute.Unit1Ar
			},
			Value2 = new()
			{
				En = productAttribute.Value2En,
				Ar = productAttribute.Value2Ar
			},
			Unit2 = new()
			{
				En = attribute.Unit2En,
				Ar = attribute.Unit2Ar
			},
			Value3 = new()
			{
				En = productAttribute.Value3En,
				Ar = productAttribute.Value3Ar
			},
			Unit3 = new()
			{
				En = attribute.Unit3En,
				Ar = attribute.Unit3Ar
			},
			Image = _mapper.Map<ImageResponse>(productAttribute.Image!),
			SortOrder = (int)attribute.SortOrder!
		};
	}

	public ISet<SalePropertyResponse> MapProperties(IEnumerable<ProductAttribute> productAttributes)
	{
		SortedSet<SalePropertyResponse> result = [];
		foreach (ProductAttribute productAttribute in productAttributes)
		{
			SalePropertyResponse property = FindPropertyOrCreate(productAttribute, result);
			AttributeEntity attribute = productAttribute.Attribute!;
			SalePropertyGroupResponse groupResult = FindPropertyGroupByNameOrCreate(attribute, property.Groups);
			groupResult.Items.Add(Map(productAttribute, attribute));
		}

		return result;
	}

	public PaginatedResponse<ProductFamilyProductItem> FindAllModelsByFamilyCode(SaleProductPageableParam param, string code)
	{
		code = Regex.Unescape(code);
		Expression<Func<ProductEntity, bool>> predicate = param.ToPredicate()
		.And(ProductEntity.QFamilyCodeEquals(code)).And(ProductEntity.QStatusEquals(EProductStatus.Published));

		Page page = new(param.Page, param.Size);
		PaginatedEntity<ProductEntity> pEntity = FindAll(page, predicate, Sort.From(param));
		IEnumerable<ProductFamilyProductItem> models = pEntity.Content.Select(_mapper.Map<ProductFamilyProductItem>);

		return PaginatedResponse<ProductFamilyProductItem>.From(pEntity, models);
	}

	public PaginatedResponse<ProductFamilyProductItem> FindAllModelsByFamilyCodePDP(SaleProductPageableParam param, string code)
	{
		Expression<Func<ProductEntity, bool>> predicate = param.ToPredicate()
		.And(ProductEntity.QFamilyCodeEquals(code)).And(ProductEntity.QStatusEquals(EProductStatus.Published));

		Page page = new(param.Page, param.Size);
		PaginatedEntity<ProductEntity> pEntity = FindAll(page, predicate, Sort.From(param));
		IEnumerable<ProductFamilyProductItem> models = pEntity.Content.Select(_mapper.Map<ProductFamilyProductItem>);

		return PaginatedResponse<ProductFamilyProductItem>.From(pEntity, models);
	}

	public List<ProductMeilisearch> FindAllSearchablesById(SaleProductPageableParam param, int id)
	{
		Expression<Func<ProductEntity, bool>> predicate = param.ToPredicate()
			.And(CommonEntity.QIdEquals<ProductEntity>(id)).And(ProductEntity.QStatusEquals(EProductStatus.Published));

		Page page = new(param.Page, param.Size);
		PaginatedEntity<ProductEntity> pEntity = FindAll(page, predicate, Sort.From(param));

		if (!pEntity.Content.Any())
		{
			return [];
		}

		List<ProductMeilisearch>? products = [];
		foreach (ProductEntity content in pEntity.Content)
		{
			ProductMeilisearch model = _mapper.Map<ProductMeilisearch>(content);

			List<SaleItemMeilisearch> saleItems = _saleItemService.FindAllByProductId(content.Id!.Value)
			.Select(_mapper.Map<SaleItemMeilisearch>).ToList();
			model.SaleItems = saleItems;
			model.Brand = content!.Brand?.Name;
			model.Category = content.Categories.FirstOrDefault()?.Path;
			model.PriceSummaries = GetPriceSummaries(saleItems);

			var attributes = content!.Attributes.Where(attr => attr.PropertyId == 12);
			var mappedAttributes = MapProperties(attributes);
			foreach (var attr in mappedAttributes)
			{
				var items = attr.Groups.SelectMany(x => x.Items);
				//model.IsAvailable = saleItems != null && model.HighestPrice.HasValue && model.LowestPrice != 0;
				model.SpecificationValuesEn = items.GroupBy(x => x.Description.En).ToDictionary(x => $"{x.Key?.Replace(" ", "_")}" ?? "", x => string.Join(" / ", x.Select(i => $"{i.Value1.En} {i.Unit1?.En ?? ""}")));
				model.SpecificationValuesAr = items.GroupBy(x => x.Description.Ar).ToDictionary(x => $"{x.Key?.Replace(" ", "_")}" ?? "", x => string.Join(" / ", x.Select(i => $"{i.Value1.Ar} {i.Unit1?.Ar ?? ""}")));
			}
			products.Add(model);
		}

		return products;
	}

	public List<ProductMeilisearch> FindAllSearchablesByFamilyCode(SaleProductPageableParam param, string code)
	{
		Expression<Func<ProductEntity, bool>> predicate = param.ToPredicate()
			.And(ProductEntity.QFamilyCodeEquals(code)).And(ProductEntity.QStatusEquals(EProductStatus.Published));

		Page page = new(param.Page, param.Size);
		PaginatedEntity<ProductEntity> pEntity = FindAll(page, predicate, Sort.From(param));

		if (!pEntity.Content.Any())
		{
			return [];
		}

		List<ProductMeilisearch>? products = [];
		foreach (ProductEntity content in pEntity.Content)
		{
			ProductMeilisearch model = _mapper.Map<ProductMeilisearch>(content);

			List<SaleItemMeilisearch> saleItems = _saleItemService.FindAllByProductId(content.Id!.Value)
			.Select(_mapper.Map<SaleItemMeilisearch>).ToList();
			model.SaleItems = saleItems;
			model.Brand = content!.Brand?.Name;
			model.Category = content.Categories.FirstOrDefault()?.Path;
			model.PriceSummaries = GetPriceSummaries(saleItems);
			var attributes = content!.Attributes.Where(attr => attr.PropertyId == 12);
			var mappedAttributes = MapProperties(attributes);
			foreach (var attr in mappedAttributes)
			{
				var items = attr.Groups.SelectMany(x => x.Items);
				//model.IsAvailable = saleItems != null && model.HighestPrice.HasValue && model.LowestPrice != 0;
				model.SpecificationValuesEn = items.GroupBy(x => x.Description.En).ToDictionary(x => $"{x.Key?.Replace(" ", "_")}" ?? "", x => string.Join(" / ", x.Select(i => $"{i.Value1.En} {i.Unit1?.En ?? ""}")));
				model.SpecificationValuesAr = items.GroupBy(x => x.Description.Ar).ToDictionary(x => $"{x.Key?.Replace(" ", "_")}" ?? "", x => string.Join(" / ", x.Select(i => $"{i.Value1.Ar} {i.Unit1?.Ar ?? ""}")));
			}
			products.Add(model);
		}

		return products;
	}

	private static Dictionary<string, PriceSummary> GetPriceSummaries(List<SaleItemMeilisearch> saleItems)
	{
		Dictionary<string, PriceSummary> priceSummaries = [];
		string[] countryNames = ["SA", "QA", "AE", "ID", "MY"];

		foreach (var country in countryNames)
		{
			decimal lowestPrice = GetMinPriceRange(saleItems, country) ?? 0;
			decimal? oriPrice = GetOriPriceRange(saleItems, country);
			decimal? highestPrice = GetMaxPriceRange(saleItems, country);
			PriceSummary priceSummary = new()
			{
				Country = country,
				Price = new()
				{
					Symbol = GetCountrySymbol(country),
					LowestPrice = lowestPrice,
					OriginalPrice = lowestPrice == oriPrice ? null : oriPrice,
					HighestPrice = lowestPrice == highestPrice ? null : highestPrice
				},
				IsAvailable = saleItems != null && lowestPrice != 0
			};
			priceSummaries.Add(country, priceSummary);
		}

		return priceSummaries;
	}

	private static LocalizableString GetCountrySymbol(string country)
	{
		return country switch
		{
			"SA" => new LocalizableString()
			{
				Ar = "ر.س",
				En = "SAR"
			},
			"QA" => new LocalizableString()
			{
				Ar = "ر.ق",
				En = "QAR"
			},
			"AE" => new LocalizableString()
			{
				Ar = "در",
				En = "AED"
			},
			"ID" => new LocalizableString()
			{
				Ar = "ر.ب",
				En = "RP"
			},
			"MY" => new LocalizableString()
			{
				Ar = "ر.م",
				En = "RM"
			},
			_ => new LocalizableString(),
		};
	}

	private static decimal? GetMinPriceRange(List<SaleItemMeilisearch> saleItems, string country)
	{
		return saleItems.Where(x => x.Country == country &&
		x.Status == EActivableStatus.Active &&
		x.Enabled.HasValue && x.Enabled.Value).Min(x => x.Price);
	}

	private static decimal? GetOriPriceRange(List<SaleItemMeilisearch> saleItems, string country)
	{
		return saleItems.Where(x => x.Country == country &&
		x.Status == EActivableStatus.Active &&
		x.Enabled.HasValue && x.Enabled.Value).Min(x => x.OriginalPrice);
	}

	private static decimal? GetMaxPriceRange(List<SaleItemMeilisearch> saleItems, string country)
	{
		return saleItems.Where(x => x.Country == country &&
		x.Status == EActivableStatus.Active &&
		x.Enabled.HasValue && x.Enabled.Value).Max(x => x.Price);
	}

	public ProductFamilyPaginatedResponse FindAllFamilies(ProductFamilyPageableParam param)
	{
		_categoryService.CollectSlugs(param.Categories);

		Expression<Func<ProductEntity, bool>> predicate = Filter(param.ToPredicate());

		Page page = new(param.Page, param.Size);
		PaginatedEntity<string?> pFamily = repository.FindAllFamilyCodes(page, predicate, Sort.From(param));

		int productCount = _repository.Count(predicate);

		return ProductFamilyPaginatedResponse.From(pFamily, productCount);
	}

	private IEnumerable<string> FindAllRelatedFamilies()
	{
		Expression<Func<ProductEntity, bool>> predicate = Filter(null);

		Random rnd = new();
		List<string> codes = _relatedFamilyDummies.GetRange(rnd.Next(6), 5);
		Expression<Func<ProductEntity, bool>>? codePredicate = null;
		foreach (string code in codes)
		{
			codePredicate = codePredicate != null
			? codePredicate.Or(ProductEntity.QFamilyCodeEquals(code))
			: ProductEntity.QFamilyCodeEquals(code);
		}

		PaginatedEntity<string?> pFamily = repository
			.FindAllFamilyCodes(new Page(0, 5), predicate.And(codePredicate!), null);

		return pFamily.Content.Select(code => code!);
	}

	public IEnumerable<string> FindAllRelatedFamiliesById(int id)
	{
		return FindAllRelatedFamilies();
	}

	public IEnumerable<string> FindAllRelatedFamiliesBySlug(string slug)
	{
		return FindAllRelatedFamilies();
	}

	protected override Expression<Func<ProductEntity, bool>> Filter(Expression<Func<ProductEntity, bool>>? predicate)
	{
		Principal? principal = _principalService.FindCurrent();
		if (principal == null || principal.Type == EPrincipalType.Individual || principal.Type == EPrincipalType.Vendor)
		{
			/*Expression<Func<ProductEntity, bool>> filterPredicate = ProductEntity.QSellableEquals(true)
            .And(ProductEntity.QStatusEquals(EProductStatus.Published));*/
			Expression<Func<ProductEntity, bool>> filterPredicate = ProductEntity.QStatusEquals(EProductStatus.Published); //TEMP
			return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
		}
		else if (principal.Type == EPrincipalType.Company)
		{
			Expression<Func<ProductEntity, bool>> filterPredicate = ProductEntity.QStatusEquals(EProductStatus.Published);
			return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
		}
		else
		{
			return predicate ?? Expressions.True<ProductEntity>();
		}
	}
}
