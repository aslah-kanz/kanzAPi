using System.Transactions;
using KanzApi.Account.Services;
using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Repositories;
using KanzApi.Transaction.Services;
using KanzApi.Utils;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Services;

public class SaleItemSyncableService(ISaleItemRepository repository,
ISaleItemSellableService service, IProductSyncableService productService, IStoreService storeService,
ISearchEngineService<ProductFamilyProductItemMeilisearch> searchEngineService)
: CrudService<SaleItem, long?>(repository), ISaleItemSyncableService
{

    private readonly ISaleItemSellableService _service = service;

    private readonly IProductSyncableService _productService = productService;

    private readonly IStoreService _storeService = storeService;

    private readonly ISearchEngineService<ProductFamilyProductItemMeilisearch> _searchEngineService = searchEngineService;

    private void CalculateProductPrice(ProductEntity product, List<SaleItem> entities)
    {
        product.LowestPrice = entities.Min(e => e.Price);
        if (product.LowestPrice != null)
        {
            decimal? highestPrice = entities.Max(e => e.Price);
            product.HighestPrice = highestPrice != product.LowestPrice ? highestPrice : null;

            product.OriginalPrice = entities.Min(e => e.OriginalPrice);
            product.Sellable = true;
        }
        else
        {
            product.HighestPrice = null;
            product.OriginalPrice = null;
            product.Sellable = false;
        }

        _productService.Edit(product);
    }

    public void AdjustProductPrice(ProductEntity product, SaleItem? entity)
    {
        List<SaleItem> entities = entity != null
        ? [.. _service.FindAllByProductId((int)product.Id!), entity]
        : [.. _service.FindAllByProductId((int)product.Id!)];
        CalculateProductPrice(product, entities);
    }

    public override SaleItem Add(SaleItem entity)
    {
        using TransactionScope scope = Transactions.Create();

        entity = base.Add(entity);

        AdjustProductPrice(entity.Product!, entity);

        _searchEngineService.SyncProductsByFamilyCode(entity.Product!.FamilyCode!);

        scope.Complete();

        return entity;
    }

    public override SaleItem Edit(SaleItem entity)
    {
        using TransactionScope scope = Transactions.Create();

        entity = base.Edit(entity);

        AdjustProductPrice(entity.Product!, entity);

        _searchEngineService.SyncProductsByFamilyCode(entity.Product!.FamilyCode!);

        scope.Complete();

        return entity;
    }

    public override SaleItem Remove(SaleItem entity)
    {
        using TransactionScope scope = Transactions.Create();

        _storeService.DecreaseSaleItemCount(entity.Store!);

        ProductEntity product = entity.Product!;

        entity = base.Remove(entity);

        AdjustProductPrice(product, null);

        _searchEngineService.SyncProductsByFamilyCode(entity.Product!.FamilyCode!);

        scope.Complete();

        return entity;
    }

    public SaleItem Reserve(long id, int value, bool withoutStock = false)
    {
        SaleItem entity = GetById(id);
        entity.ReservedStock += value;

        if (!withoutStock)
        {
            entity.Stock -= value;

            if (entity.Stock < 0)
            {
                throw new SaleItemOutOfStockException((int)entity.ProductId!, (int)entity.Stock + value);
            }
        }

        return Edit(entity);
    }

    public SaleItem Release(long id, int value)
    {
        SaleItem entity = GetById(id);
        entity.ReservedStock -= value;

        if (entity.ReservedStock < 0)
        {
            throw new SaleItemOutOfStockException((int)entity.ProductId!, (int)entity.ReservedStock + value);
        }

        return Edit(entity);
    }

    public SaleItem Restore(long id, int value, bool resetStock = false)
    {
        var entity = GetById(id);
        entity.ReservedStock -= value;
        entity.Stock += resetStock ? -entity.Stock : value;

        if (entity.ReservedStock < 0)
        {
            throw new SaleItemOutOfStockException((int)entity.ProductId!, (int)entity.ReservedStock + value);
        }

        return Edit(entity);
    }
}
