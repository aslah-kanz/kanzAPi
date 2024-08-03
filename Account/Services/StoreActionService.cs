using System.Transactions;
using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Services;
using KanzApi.Utils;
using MapsterMapper;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Account.Services;

public class StoreActionService(IStoreFilterableService service, IMapper mapper,
ISaleItemSellableService saleItemService, ISaleItemFilterableService saleItemFilteredService,
IProductSyncableService productService) : IStoreActionService
{

    private readonly IStoreFilterableService _service = service;

    private readonly IMapper _mapper = mapper;

    private readonly ISaleItemSellableService _saleItemService = saleItemService;

    private readonly ISaleItemFilterableService _saleItemFilteredService = saleItemFilteredService;

    private readonly IProductSyncableService _productService = productService;

    public StoreResponse Inactivate(int id)
    {
        Store entity = _service.GetById(id);

        using TransactionScope scope = Transactions.Create();

        IEnumerable<SaleItem> saleItems = _saleItemFilteredService.FindAllByStoreId(id);
        foreach (SaleItem saleItem in saleItems)
        {
            _saleItemFilteredService.Inactivate((long)saleItem.Id!);

            IEnumerable<SaleItem> entities = _saleItemService.FindAllByProductId((int)saleItem.ProductId!);
            int activeCount = entities.Count();
            if (activeCount == 0)
            {
                ProductEntity pEntity = _productService.GetById((int)saleItem.ProductId!);
                pEntity.Sellable = false;
                _productService.Edit(pEntity);
            }
        }

        _service.Inactivate(entity);

        StoreResponse response = _mapper.Map<StoreResponse>(entity);

        scope.Complete();

        return response;
    }

    public StoreResponse Activate(int id)
    {
        Store entity = _service.GetById(id);

        using TransactionScope scope = Transactions.Create();

        IEnumerable<SaleItem> saleItems = _saleItemFilteredService.FindAllByStoreId(id);
        foreach (SaleItem saleItem in saleItems)
        {
            _saleItemFilteredService.Activate((long)saleItem.Id!);

            ProductEntity pEntity = _productService.GetById((int)saleItem.ProductId!);
            pEntity.Sellable = true;
            _productService.Edit(pEntity);
        }

        _service.Activate(entity);

        StoreResponse response = _mapper.Map<StoreResponse>(entity);

        scope.Complete();

        return response;
    }
}
