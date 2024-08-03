using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Repositories;
using MapsterMapper;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.Linq.Expressions;

namespace KanzApi.Product.Services;

public class ProductSaleItemFilterableService(IProductRepository repository, IMapper mapper,
 IPrincipalService principalService, IPrincipalDetailFilterableService principalDetailService,
 ISaleItemFilterableService saleItemService)
: FilterableCrudService<ProductEntity, int?>(repository), IProductSaleItemFilterableService
{

    private readonly IMapper _mapper = mapper;

    private readonly ISaleItemFilterableService _saleItemService = saleItemService;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    public PaginatedResponse<ProductItem> FindAllModels(ProductPageableParam param)
    {
        PaginatedEntity<ProductEntity> pEntity = FindAll(param);
        IEnumerable<ProductItem> models = pEntity.Content.Select(e =>
        {
            ProductItem model = _mapper.Map<ProductItem>(e);
            model.StoreIds = _saleItemService.FindAllByProductId((int)e.Id!)
            .Select(i => (long)i.StoreId!).ToList();

            return model;
        }).ToArray();

        return PaginatedResponse<ProductItem>.From(pEntity, models);
    }

    protected override Expression<Func<ProductEntity, bool>> Filter(Expression<Func<ProductEntity, bool>>? predicate)
    {
        Principal? principal = _principalService.FindCurrent();
        if (principal == null)
        {
            throw new PrincipalNotAllowedException();
        }
        else if (principal.Type == EPrincipalType.Company || principal.Type == EPrincipalType.Individual)
        {
            throw new PrincipalTypeNotAllowedException((EPrincipalType)principal.Type);
        }

        if (principal.Type == EPrincipalType.Vendor)
        {
            Expression<Func<ProductEntity, bool>> filterPredicate = ProductEntity.QStatusEquals(EProductStatus.Published);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else if (principal.Type == EPrincipalType.Manufacture)
        {
            PrincipalDetail principalDetail = _principalDetailService.GetByCurrentPrincipal();
            Expression<Func<ProductEntity, bool>> filterPredicate = ProductEntity.QPrincipalDetailIdEquals((int)principalDetail.Id!);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else
        {
            return predicate ?? Expressions.True<ProductEntity>();
        }
    }
}
