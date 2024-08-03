using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Services;
using Mapster;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Configurations.Mapping;

public class MapsterRegister : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<int?, Brand?>()
        .MapWith(src => src != null ? MapContext.Current.GetService<IBrandService>().GetById(src) : null);
        config.NewConfig<int, Category>()
        .MapWith(src => MapContext.Current.GetService<ICategoryService>().GetById(src));
        config.NewConfig<int?, Category?>()
        .MapWith(src => src != null ? MapContext.Current.GetService<ICategoryService>().GetById(src) : null);
        config.NewConfig<int?, Country>()
        .MapWith(src => MapContext.Current.GetService<ICountryService>().GetById(src));
        config.NewConfig<int?, Currency>()
        .MapWith(src => MapContext.Current.GetService<ICurrencyService>().GetById(src));
        config.NewConfig<Guid?, CustomerOrder>()
        .MapWith(src => MapContext.Current.GetService<ICustomerOrderFilterableService>().GetById(src));
        config.NewConfig<Guid?, Transaction.Entities.CustomerOrderItem>()
        .MapWith(src => MapContext.Current.GetService<ICustomerOrderItemService>().GetById(src));
        config.NewConfig<int, Department>()
        .MapWith(src => MapContext.Current.GetService<IDepartmentFilterableService>().GetById(src));
        config.NewConfig<long?, Document?>()
        .MapWith(src => src != null ? MapContext.Current.GetService<IDocumentService>().GetById(src) : null);
        config.NewConfig<Guid?, Exchange>()
        .MapWith(src => MapContext.Current.GetService<IExchangeFilterableService>().GetById(src));
        config.NewConfig<int?, FaqGroup>()
        .MapWith(src => MapContext.Current.GetService<IFaqGroupService>().GetById(src));
        config.NewConfig<long?, Image?>()
        .MapWith(src => src != null ? MapContext.Current.GetService<IImageService>().GetById(src) : null);
        config.NewConfig<int?, JobField>()
        .MapWith(src => MapContext.Current.GetService<IJobFieldService>().GetById(src));
        config.NewConfig<int?, Principal?>()
        .MapWith(src => src != null ? MapContext.Current.GetService<IPrincipalService>().GetById(src) : null);
        config.NewConfig<int?, PrincipalDetail>()
        .MapWith(src => MapContext.Current.GetService<IPrincipalDetailFilterableService>().GetById(src));
        config.NewConfig<int, Privilege>()
        .MapWith(src => MapContext.Current.GetService<IPrivilegeService>().GetById(src));
        config.NewConfig<int?, ProductEntity?>()
        .MapWith(src => src != null ? MapContext.Current.GetService<IProductSyncableService>().GetById(src) : null);
        config.NewConfig<Guid?, ProductReview>()
        .MapWith(src => MapContext.Current.GetService<IProductReviewFilterableService>().GetById(src));
        config.NewConfig<Guid?, PurchaseQuote>()
        .MapWith(src => MapContext.Current.GetService<IPurchaseQuoteFilterableService>().GetById(src));
        config.NewConfig<int, Role>()
        .MapWith(src => MapContext.Current.GetService<IRoleService>().GetById(src));
        config.NewConfig<int?, SaleItem>()
        .MapWith(src => MapContext.Current.GetService<ISaleItemFilterableService>().GetById(src));
        config.NewConfig<int?, Store>()
        .MapWith(src => MapContext.Current.GetService<IStoreFilterableService>().GetById(src));

        config.NewConfig<CommonEntity<int?>, int?>()
        .MapWith(src => src.Id);
        config.NewConfig<CommonEntity<long?>, long?>()
        .MapWith(src => src.Id);
        config.NewConfig<List<PurchaseQuote>, IEnumerable<CustomerOrderPurchaseQuoteResponse>>()
        .MapWith(src => src.Where(e => e.Status >= EPurchaseQuoteStatus.Accepted)
        .Select(e => e.Adapt<CustomerOrderPurchaseQuoteResponse>()));

        config.NewConfig<Image, ImageResponse>()
        .AfterMapping((src, dest) =>
        {
            IConfiguration configuration = MapContext.Current.GetService<IConfiguration>();
            string baseUrl = configuration.GetValue<string>("AzureStorage:BaseUrl")!;
            dest.Url = src.ToUrl(baseUrl);
        });

        config.NewConfig<SaleItem, SaleItemMeilisearch>()
        .Map(dest => dest.Country, src => src.Store != null ? src.Store.Country : null);

        config.NewConfig<Category, OverviewCategoryItem>().Ignore(src => src.Products);
    }
}
