using KanzApi.Account.Services;
using KanzApi.Common.Services;
using KanzApi.Configurations.Contexts;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Repositories;
using KanzApi.Utils;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Transaction.Services;

public class CartFilterableService(ICartRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService,
ISaleItemSyncableService saleItemService, ISaleItemFilterableService saleItemFilteredService, CommonDbContext commonDbContext)
: FilterableCrudService<Cart, int?>(repository), ICartFilterableService
{

    private readonly CommonDbContext _commonDbContext = commonDbContext;
    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private readonly ISaleItemSyncableService _saleItemService = saleItemService;

    private readonly ISaleItemFilterableService _saleItemFilteredService = saleItemFilteredService;

    public CartResponse Add(AddCartRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Cart? entity = FindByProductIdAndPrice((int)request.Product!, request.Price);
        if (entity == null)
        {
            entity = _mapper.Map<Cart>(request);
            entity.Principal = _principalService.GetCurrent();

            foreach (int id in request.SaleItemIds)
            {
                SaleItem item = _saleItemFilteredService.GetById(id);

                if (request.Product != item.ProductId) throw new ProductMismatchException((int)request.Product!, (int)item.ProductId!);
                if (request.Price != item.Price) throw new SaleItemPriceMismatchException((int)request.Price!, (decimal)item.Price!);

                CartSaleItem cartItem = new()
                {
                    Cart = entity,
                    SaleItem = item
                };
                entity.Stock += item.Stock;
                entity.CartSaleItems.Add(cartItem);
            }

            entity = Add(entity);
        }
        else
        {
            int quantity = (int)entity.Quantity!;
            _mapper.Map(request, entity);

            entity.Quantity += quantity;
            Edit(entity);
        }

        if (entity.Quantity > entity.Stock) throw new SaleItemOutOfStockException((int)request.Product!, (int)entity.Stock!);

        CartResponse response = _mapper.Map<CartResponse>(entity);

        scope.Complete();

        return response;
    }

    public CartResponse Edit(int id, EditCartRequest request)
    {
        FindAllModels();

        using TransactionScope scope = Transactions.Create();

        Cart entity = GetById(id);
        _mapper.Map(request, entity);

        if (entity.Quantity > entity.Stock) throw new SaleItemOutOfStockException((int)entity.ProductId!, (int)entity.Stock!);

        entity = Edit(entity);

        CartResponse response = _mapper.Map<CartResponse>(entity);

        scope.Complete();

        return response;
    }

    public CartResponse RemoveModelById(int id)
    {
        Cart entity = RemoveById(id);
        return _mapper.Map<CartResponse>(entity);
    }

    public int RemoveAllByProductIds(ISet<int> productIds)
    {
        return RemoveAllByPredicate(Cart.QProductIdEquals(productIds));
    }

    public CartResponse GetModelById(int id)
    {
        FindAllModels();
        Cart entity = GetById(id);
        return _mapper.Map<CartResponse>(entity);
    }

    public Cart? FindByProductIdAndPrice(int productId, decimal price)
    {
        return FindByPredicate(Cart.QProductIdEquals(productId).And(Cart.QPriceEquals(price)));
    }

    protected override Expression<Func<Cart, bool>> Filter(Expression<Func<Cart, bool>>? predicate)
    {
        Expression<Func<Cart, bool>> filterPredicate = Cart.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public CartsResponse FindAllModels()
    {
        CartsResponse response = new CartsResponse();
        var pEntity = FindAll().AsEnumerable().ToList();
        bool isChanged = false;
        foreach (var item in pEntity.ToList())
        {

            var ids = item.CartSaleItems.Select(d => d.SaleItemId).ToList();
            var saleItems = _saleItemService.FindAll(e => ids.Contains(e.Id) && e.Enabled == true
            && e.Status == Common.Entities.EActivableStatus.Active, null);
            var availableStock = saleItems.Sum(e => e.Stock);

            foreach (var cartSaleItem in item.CartSaleItems.ToList())
            {
                var saleItem = saleItems.Where(e => cartSaleItem.CartId == item.Id && cartSaleItem.SaleItemId == e.Id).FirstOrDefault();
                if (saleItem != null)
                {
                    cartSaleItem.Stock = saleItem.Stock;
                    _commonDbContext.CartSaleItems.Update(cartSaleItem);
                }
                else
                {
                    _commonDbContext.CartSaleItems.Remove(cartSaleItem);
                }
                _commonDbContext.SaveChanges();

            }

            if (availableStock == 0)
            {
                Remove(item);
                isChanged = true;
            }
            else
            {
                item.Stock = availableStock;
                if (availableStock < item.Quantity)
                {
                    item.Quantity = availableStock;
                    isChanged = true;
                }
                Edit(item);
            }

        }
        response.Items = pEntity.AsEnumerable().Select(_mapper.Map<CartResponse>).ToList();
        response.IsCartChanges = isChanged;
        return response;
    }
}
