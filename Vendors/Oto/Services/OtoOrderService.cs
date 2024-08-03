using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.Net.Http.Headers;

namespace KanzApi.Vendors.Oto.Services;

public class OtoOrderService(IHttpClientFactory httpClientFactory, ILogger<OtoOrderService> logger,
IServiceProvider serviceProvider, IConfiguration configuration, IOtoAuthorizationService otoAuthorizationService)
: OtoService(httpClientFactory, logger), IOtoOrderService
{

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private readonly IConfiguration _configuration = configuration;

    private readonly IOtoAuthorizationService _otoAuthorizationService = otoAuthorizationService;

    public OtoOrderResponse Create(OtoOrderRequest request)
    {
        using HttpClient client = _httpClientFactory.CreateClient(Constants.OtoClient);

        OtoRefreshTokenResponse token = _otoAuthorizationService.RefreshToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return Post<OtoOrderRequest, OtoOrderResponse>(
            client, "/rest/v2/createOrder", request)!;
    }

    public OtoOrderResponse Create(DeliveryOptionItem item, StoreOrder storeOrder, List<PurchaseQuote> purchaseQuotes)
    {
        CustomerOrder order = storeOrder.CustomerOrder!;
        Principal customer = order.Principal!;
        Store store = storeOrder.Store!;

        using IServiceScope scope = _serviceProvider.CreateScope();
        IPrincipalAddressService principalAddressService = scope
        .ServiceProvider.GetRequiredService<IPrincipalAddressService>();
        PrincipalAddress address = principalAddressService.GetDefaultByPrincipalId((int)customer.Id!);

        double totalAmount = 0;
        int totalQuantity = 0;
        List<OtoItemRequest> items = [];
        foreach (PurchaseQuote purchaseQuote in purchaseQuotes)
        {
            ProductEntity product = purchaseQuote.Product!;
            int quantity = (int)purchaseQuote.ConfirmedQuantity!;
            totalAmount += Decimal.ToDouble((decimal)purchaseQuote.Price! * quantity);
            totalQuantity += quantity;
            items.Add(new()
            {
                Name = product.Name!.En,
                Price = Decimal.ToDouble((decimal)purchaseQuote.Price!),
                Quantity = purchaseQuote.ConfirmedQuantity,
                Sku = purchaseQuote.VendorSku
            });
        }

        OtoOrderRequest request = new()
        {
            OrderId = storeOrder.InvoiceNumber,
            PickupLocationCode = store.Code,
            CreateShipment = _configuration.GetValue<bool>("Oto:ShipmentEnabled"),
            DeliveryOptionId = item.Id!.ToString(),
            StoreName = store.OtoStoreName,
            PaymentMethod = "paid",
            Amount = totalAmount,
            AmountDue = 0,
            Currency = "SAR",
            PackageCount = storeOrder.PackageCount,
            PackageWeight = item.Weight,
            OrderDate = order.UpdatedAt,
            SenderName = store.Name,
            Customer = new()
            {
                Name = customer.FullName,
                Email = customer.Email,
                Mobile = customer.FullPhoneNumber,
                Address = address.Address,
                City = address.City,
                Country = address.Country,
                Latitude = Decimal.ToDouble((decimal)address.Latitude!),
                Longitude = Decimal.ToDouble((decimal)address.Longitude!)
            },
            Items = items
        };

        return Create(request);
    }

    public OtoOrderHistoryResponse FindAllHistories(OtoOrderHistoryRequest request)
    {
        using HttpClient client = _httpClientFactory.CreateClient(Constants.OtoClient);

        OtoRefreshTokenResponse token = _otoAuthorizationService.RefreshToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return Post<OtoOrderHistoryRequest, OtoOrderHistoryResponse>(
            client, "/rest/v2/orderHistory", request)!;
    }

    public OtoOrderHistoryResponse FindAllHistories(params string[] invoiceNumbers)
    {
        return FindAllHistories(new OtoOrderHistoryRequest() { OrderIds = [.. invoiceNumbers] });
    }
}
