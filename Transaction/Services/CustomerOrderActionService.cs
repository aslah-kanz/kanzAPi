using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Services;
using KanzApi.Configurations.Contexts;
using KanzApi.Product.Entities;
using KanzApi.Product.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Builders;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using KanzApi.Vendors.Oto.Services;
using MapsterMapper;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.Transactions;

namespace KanzApi.Transaction.Services;

public class CustomerOrderActionService(ICustomerOrderService service,
IMapper mapper, IConfiguration configuration, IPurchaseQuoteService purchaseQuoteService,
ICartFilterableService cartService, IPrincipalAddressFilterableService principalAddressService, CommonDbContext commonDbContext,
IOtoCheckoutService otoCheckoutService, ICodeGenerator codeGenerator, ISaleItemFilterableService saleItemService)
: ICustomerOrderActionService
{

    private readonly CommonDbContext _commonDbContext = commonDbContext;
    private readonly ICustomerOrderService _service = service;

    private readonly IMapper _mapper = mapper;

    private readonly IConfiguration _configuration = configuration;

    private readonly IPurchaseQuoteService _purchaseQuoteService = purchaseQuoteService;

    private readonly ICartFilterableService _cartService = cartService;

    private readonly IPrincipalAddressFilterableService _principalAddressService = principalAddressService;

    private readonly IOtoCheckoutService _otoCheckoutService = otoCheckoutService;

    private readonly ISaleItemFilterableService _saleItemService = saleItemService;

    private readonly ICodeGenerator _codeGenerator = codeGenerator;

    private void AddPurchaseQuote(Entities.CustomerOrderItem item, CartSaleItem cartSaleItem, int quantity)
    {
        CustomerOrder entity = item.CustomerOrder!;
        Cart cart = cartSaleItem.Cart!;
        Store store = cartSaleItem.Store!;
        StoreOrder? storeOrder = entity.StoreOrders.FirstOrDefault(e => e.StoreId == store.Id);
        EPurchaseQuoteStatus status = quantity > 0 ? EPurchaseQuoteStatus.WaitingPayment : EPurchaseQuoteStatus.Unassigned;
        if (storeOrder == null)
        {
            storeOrder = new()
            {
                InvoiceNumber = _codeGenerator.Generate("PQ", 6),
                CustomerOrder = entity,
                Store = store,
                PurchaseQuoteStatus = status
            };
            entity.StoreOrders.Add(storeOrder);
        }

        PurchaseQuote purchaseQuote = new()
        {
            CustomerOrder = item.CustomerOrder,
            CustomerOrderItem = item,
            CartId = cart.Id,
            SaleItemId = cartSaleItem.SaleItemId,
            VendorSku = cartSaleItem.VendorSku,
            StoreOrder = storeOrder,
            Store = store,
            Product = cart.Product,
            RequestedQuantity = quantity,
            TotalRequestedQuantity = cart.Quantity,
            Price = cart.Price,
            MinPrice = cartSaleItem.MinPrice,
            MaxPrice = cartSaleItem.MaxPrice,
            DiscountPrice = cartSaleItem.DiscountPrice,
            Stock = cartSaleItem.Stock,
            MinOrderQuantity = cartSaleItem.MinOrderQuantity,
            MaxOrderQuantity = cartSaleItem.MaxOrderQuantity,
            Status = status
        };
        item.PurchaseQuotes.Add(purchaseQuote);
    }

    private void AddItem(CustomerOrder entity, Cart cart)
    {
        int quantity = (int)cart.Quantity!;

        Entities.CustomerOrderItem item = new()
        {
            CustomerOrder = entity,
            CartId = cart.Id,
            Product = cart.Product,
            Quantity = quantity,
            Price = cart.Price,
            SubTotal = quantity * cart.Price,
            Comment = cart.Comment
        };
        entity.Items.Add(item);

        foreach (CartSaleItem cartSaleItem in cart.CartSaleItems.OrderByDescending(e => e.Stock))
        {
            int stock = (int)cartSaleItem.Stock!;
            AddPurchaseQuote(item, cartSaleItem, quantity >= stock ? stock : quantity);

            if (quantity >= stock) quantity -= stock;
            else if (quantity > 0) quantity = 0;

            cartSaleItem.Stock -= quantity;

        }
    }

    private void Map(PurchaseQuote purchaseQuote, Dictionary<int, DeliveryDetail> detailMap)
    {
        int quantity = (int)purchaseQuote.RequestedQuantity!;
        if (quantity == 0) return;

        int storeId = (int)purchaseQuote.StoreId!;
        detailMap.TryGetValue(storeId, out DeliveryDetail? detail);
        if (detail == null)
        {
            CustomerOrder entity = purchaseQuote.CustomerOrder!;
            Store store = purchaseQuote.Store!;

            detail = new()
            {
                StoreId = purchaseQuote.StoreId,
                OriginCity = store.City,
                DestinationCity = entity.Address!.City,
                OriginCountry = store.Country,
                DestinationCountry = entity.Address!.Country
            };
            detailMap[storeId] = detail;
        }

        ProductEntity product = purchaseQuote.Product!;
        detail.Weight += Math.Round((double)product.Weight! * quantity, 2);
        detail.Volume += Math.Round((double)(product.Length! * product.Width! * product.Height!) * quantity, 2);
    }

    private void AdjustDeliveryOptions(CustomerOrder entity)
    {
        Dictionary<int, DeliveryDetail> detailMap = [];
        foreach (Entities.CustomerOrderItem item in entity.Items)
        {
            foreach (PurchaseQuote purchaseQuote in item.PurchaseQuotes)
            {
                Map(purchaseQuote, detailMap);
            }
        }

        DeliveryOptionsBuilder builder = new(_mapper, _configuration);
        foreach (DeliveryDetail detail in detailMap.Values)
        {
            OtoDeliveryFeeResponse response = _otoCheckoutService.CheckOtoDeliveryFee(detail);
            builder.Add(detail, response.DeliveryCompanies);
        }
        entity.DeliveryOptions = builder.Build().ToList();

        DeliveryOption? option = entity.DeliveryOptions.FirstOrDefault();
        _service.SetDeliveryOption(entity, option?.Id);
    }

    public CustomerOrderCheckoutResponse ChangeAddress(int addressId)
    {
        using TransactionScope scope = Transactions.Create();

        CustomerOrder entity = _service.GetCurrent();
        entity.Address = _principalAddressService.GetById(addressId);

        AdjustDeliveryOptions(entity);
        entity = _service.Edit(entity);

        CustomerOrderCheckoutResponse response = _mapper.Map<CustomerOrderCheckoutResponse>(entity);

        scope.Complete();

        return response;
    }

    public CustomerOrderCheckoutResponse Checkout()
    {
        List<Cart> carts = _cartService.FindAll().ToList();
        bool IsCartChanges = false;
        if (carts.Count == 0)
        {
            throw new EmptyCartException();
        }

        using TransactionScope scope = Transactions.Create();

        CustomerOrder entity = _service.Create();
        CustomerOrderCheckoutResponse response = _mapper.Map<CustomerOrderCheckoutResponse>(entity);
        var removedCarts = new List<Cart>();

        foreach (Cart cart in carts)
        {
            var saleItems = _saleItemService
            .FindAllByProductId(cart.ProductId!.Value)
            .Where(d => d.Status == Common.Entities.EActivableStatus.Active && d.Enabled == true && d.Price == cart.Price);

            int? availableStock = saleItems.Sum(e => e.Stock);

            if (!saleItems.Any())
            {
                _cartService.Remove(cart);
                _commonDbContext.CartSaleItems.RemoveRange(cart.CartSaleItems);
                IsCartChanges = true;
                _commonDbContext.SaveChanges();
                removedCarts.Add(cart);
                continue;
            }

            foreach (SaleItem item in saleItems)
            {
                var cartSaleItem = _commonDbContext.CartSaleItems.Where(e => e.CartId == cart.Id && e.SaleItemId == item.Id).FirstOrDefault();

                if (cartSaleItem != null)
                {
                    cartSaleItem.Stock = item.Stock;
                    _commonDbContext.CartSaleItems.Update(cartSaleItem);
                }
            }
            _commonDbContext.SaveChanges();

            cart.Stock = availableStock;

            if (availableStock < cart.Quantity)
            {
                cart.Quantity = availableStock;
                IsCartChanges = true;
                if (availableStock == 0)
                {
                    _cartService.Remove(cart);
                    removedCarts.Add(cart);
                    continue;
                }
                else
                {
                    _cartService.Edit(cart);
                };

            }

            AddItem(entity, cart);

        }

        foreach (Cart cart in removedCarts)
        {
            carts.Remove(cart);
        }

        if (carts.Count == 0)
        {
            response.IsCartChanges = IsCartChanges;
            return response;
        }

        entity.SubTotal = entity.Items.Sum(e => e.Quantity * e.Price);
        entity.HiglightedProduct = entity.Items.Count() > 0 ? entity.Items.First().Product : null;
        entity.InvoiceNumber = _codeGenerator.Generate("CO", 6);

        AdjustDeliveryOptions(entity);

        entity = entity.Id != null ? _service.Edit(entity) : _service.Add(entity);

        response = _mapper.Map<CustomerOrderCheckoutResponse>(entity);
        response.IsCartChanges = IsCartChanges;
        scope.Complete();

        return response;
    }

    public CustomerOrderCheckoutResponse BuyNow(CustomerOrderBuyNowRequest request)
    {
        List<Cart> carts = [];
        foreach (var saleItem in request.SaleItems)
        {
            var param = new AddCartRequest()
            {
                Comment = saleItem.Comment,
                Price = saleItem.Price,
                Quantity = saleItem.Quantity,
                Product = request.ProductId
            };
            param.SaleItemIds.Add(saleItem.SaleItemId);

            CartResponse cart = _cartService.Add(param);
            carts.Add(_mapper.Map<Cart>(cart));
        }

        bool IsCartChanges = false;

        using TransactionScope scope = Transactions.Create();

        CustomerOrder entity = _service.Create();
        foreach (Cart cart in carts)
        {
            int? availableStock = _saleItemService
            .FindAllByProductId(cart.ProductId!.Value)
            .Where(d => d.Status == Common.Entities.EActivableStatus.Active && d.Enabled == true)
            .Sum(e => e.Stock);

            cart.Stock = availableStock;

            if (availableStock < cart.Quantity)
            {
                cart.Quantity = availableStock;
                IsCartChanges = true;
                if (availableStock == 0)
                {
                    _cartService.Remove(cart);
                }
                else
                {
                    _cartService.Edit(cart);
                };

            }
            else
            {
                AddItem(entity, cart);
            }
        }

        entity.SubTotal = entity.Items.Sum(e => e.Quantity * e.Price);
        entity.HiglightedProduct = entity.Items.Count() > 0 ? entity.Items.First().Product : null;
        entity.InvoiceNumber = _codeGenerator.Generate("CO", 6);

        AdjustDeliveryOptions(entity);
        entity = entity.Id != null ? _service.Edit(entity) : _service.Add(entity);

        CustomerOrderCheckoutResponse response = _mapper.Map<CustomerOrderCheckoutResponse>(entity);
        response.IsCartChanges = IsCartChanges;
        scope.Complete();

        return response;
    }

    public void Cancel(Guid id)
    {
        CustomerOrder entity = _service.GetById(id);
        if (!entity.Cancelable)
        {
            throw new InvalidStateChangeException(
                entity.Status.ToString()!, ECustomerOrderStatus.Canceled.ToString());
        }

        using TransactionScope scope = Transactions.Create();

        _purchaseQuoteService.UpdateAllAvailableStatusesByCustomerOrderId(id, EPurchaseQuoteStatus.Canceled);

        entity.Status = ECustomerOrderStatus.Canceled;
        _service.Edit(entity);

        scope.Complete();
    }
}
