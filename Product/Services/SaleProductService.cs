using KanzApi.Common.Entities;
using KanzApi.Common.Services;
using KanzApi.Product.Models;
using KanzApi.Product.Repositories;
using MapsterMapper;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.Linq.Expressions;

namespace KanzApi.Product.Services;

public class SaleProductService(IProductRepository repository, IMapper mapper,
ISaleProductFilterableService filterableService) : CrudService<ProductEntity, int?>(repository), ISaleProductService
{

	private readonly IMapper _mapper = mapper;

	private readonly ISaleProductFilterableService _filterableService = filterableService;

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

	private SaleProductPriceListResponse MapPriceList(ProductEntity entity, string? countryCode = null)
	{
		SaleProductPriceListResponse model = _mapper.Map<SaleProductPriceListResponse>(entity);
		model.Prices = _filterableService.FindAllPricesById((int)entity.Id!, countryCode);

		return model;
	}

	private ProductEntity? FindByPredicateWithDetails(Expression<Func<ProductEntity, bool>> predicate)
	{
		return repository.FindByPredicateWithDetails(predicate);
	}

	private ProductEntity GetByIdWithDetails(int id)
	{
		return repository.FindByPredicateWithDetails(CommonEntity.QIdEquals<ProductEntity>(id))
		?? throw EntityNotFoundException.From(typeof(ProductEntity), id);
	}

	public SaleProductResponse GetModelById(int id)
	{
		ProductEntity entity = GetByIdWithDetails(id);
		return _filterableService.Map(entity);
	}

	public SaleProductPriceListResponse GetProductPriceListById(int id)
	{
		ProductEntity entity = GetById(id);
		return MapPriceList(entity);
	}

	public SaleProductPriceListResponse GetProductPriceListByIdAndCountryCode(int id, string countryCode)
	{
		ProductEntity entity = GetById(id);
		return MapPriceList(entity, countryCode);
	}

	public SaleProductResponse GetModelBySlug(string slug)
	{
		Expression<Func<ProductEntity, bool>> predicate = ProductEntity.QSlugEquals(slug);

		ProductEntity entity = FindByPredicateWithDetails(predicate)
		?? throw EntityNotFoundException.From(typeof(ProductEntity), "Slug", slug);

		return _filterableService.Map(entity);
	}
}
