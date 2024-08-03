using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Configurations.Contexts;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Repositories;
using KanzApi.Vendors.Oto.Models;
using KanzApi.Vendors.Oto.Services;
using MapsterMapper;

namespace KanzApi.Transaction.Services;

public class StoreOrderService(IStoreOrderRepository repository, CommonDbContext commonDbContext,
IMapper mapper, IOtoOrderService otoOrderService)
: CrudService<StoreOrder, Guid?>(repository), IStoreOrderService
{

    private readonly IMapper _mapper = mapper;

    private readonly IOtoOrderService _otoOrderService = otoOrderService;

    public StoreOrder IncreaseProductCount(StoreOrder entity)
    {
        entity.ProductCount++;
        return Edit(entity);
    }

    public StoreOrder DecreaseProductCount(StoreOrder entity)
    {
        entity.ProductCount--;
        return Edit(entity);
    }

    public StoreOrder GetByInvoiceNumber(string invoiceNumber)
    {
        return FindByPredicate(StoreOrder.QInvoiceNumberEquals(invoiceNumber))
        ?? throw EntityNotFoundException.From(typeof(StoreOrder), "Invoice Number", invoiceNumber);
    }

    public DeliveryHistoryResponse FindHistoryByInvoiceNumber(string invoiceNumber)
    {
        StoreOrder entity = GetByInvoiceNumber(invoiceNumber);
        OtoOrderHistoryResponse response = _otoOrderService.FindAllHistories(entity.InvoiceNumber!);

        return _mapper.Map<DeliveryHistoryResponse>(response.Items!.First());
    }

    public AdminStoreOrderItemDetail GetModelById(Guid id)
    {
        AdminStoreOrderItemDetail entity = commonDbContext.StoreOrders.Where(e => e.Id == id).Select(e => new AdminStoreOrderItemDetail()
        {
            GrandTotal = commonDbContext.PurchaseQuotes.Where(p => p.StoreOrderId == e.Id).Sum(e => e.SubTotal),
            Id = e.Id.Value,
            Invoice = e.InvoiceNumber,
            OrderDate = e.CreatedAt.Value,
            Status = e.PurchaseQuoteStatus.ToString(),
            Vendor = e.Store.Name,
            Address = e.CustomerOrder.Address.Address,
            CustomerName = e.CustomerOrder.Principal.FullName,
            ProductList = commonDbContext.PurchaseQuotes.Where(p => p.StoreOrderId == e.Id).Select(p => new AdminStoreOrderItemDetailProductList()
            {
                Brand = p.Product.Brand.NameEn,
                Price = p.Price.Value,
                Id = p.Id.Value,
                ProductName = p.Product.Name.En,
                Qty = p.RequestedQuantity.Value,
            }).ToList()
        }).First();

        return entity;
    }

    public PaginatedResponse<AdminStoreOrderItem> FindAllModels(Page page)
    {
        IEnumerable<AdminStoreOrderItem> entities = commonDbContext.StoreOrders
        .Where(e => e.PurchaseQuoteStatus != EPurchaseQuoteStatus.Delivered)
        .Skip(page.Index * page.Size).Take(page.Size).OrderByDescending(e => e.CreatedAt).Select(e => new AdminStoreOrderItem()
        {
            GrandTotal = commonDbContext.PurchaseQuotes.Where(p => p.StoreOrderId == e.Id).Sum(e => e.SubTotal),
            Id = e.Id.Value,
            Invoice = e.InvoiceNumber,
            OrderDate = e.CreatedAt.Value,
            Status = e.PurchaseQuoteStatus.ToString(),
            Vendor = e.Store.Name
        });

        int count = commonDbContext.StoreOrders
        .Where(e => e.PurchaseQuoteStatus != EPurchaseQuoteStatus.Delivered).Count();

        var data = new PaginatedEntity<AdminStoreOrderItem>(entities, page.Index, page.Size, count);

        return PaginatedResponse<AdminStoreOrderItem>.From(data, _mapper.Map<IEnumerable<AdminStoreOrderItem>>(data.Content));
    }
}
