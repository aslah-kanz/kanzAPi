using KanzApi.Account.Repositories;
using KanzApi.Common.Repositories;
using KanzApi.Configurations.Contexts;
using KanzApi.Product.Repositories;
using KanzApi.Security.Repositories;
using KanzApi.Transaction.Repositories;

namespace KanzApi.Configurations;

public static class RepositoryConfig
{

    public static WebApplicationBuilder ConfigureRepository(this WebApplicationBuilder builder)
    {
        builder.Services
        .AddDbContext<CommonDbContext>()
        .AddScoped<IAttributeRepository, AttributeRepository>()
        .AddScoped<IBannerRepository, BannerRepository>()
        .AddScoped<IBlogRepository, BlogRepository>()
        .AddScoped<IBrandRepository, BrandRepository>()
        .AddScoped<ICartRepository, CartRepository>()
        .AddScoped<ICatalogueRepository, CatalogueRepository>()
        .AddScoped<ICategoryRepository, CategoryRepository>()
        .AddScoped<ICertificateRepository, CertificateRepository>()
        .AddScoped<ICountryRepository, CountryRepository>()
        .AddScoped<ICurrencyRepository, CurrencyRepository>()
        .AddScoped<ICustomerOrderItemRepository, CustomerOrderItemRepository>()
        .AddScoped<ICustomerOrderRepository, CustomerOrderRepository>()
        .AddScoped<IDepartmentRepository, DepartmentRepository>()
        .AddScoped<IDocumentRepository, DocumentRepository>()
        .AddScoped<IExchangeRepository, ExchangeRepository>()
        .AddScoped<IFaqGroupRepository, FaqGroupRepository>()
        .AddScoped<IFaqRepository, FaqRepository>()
        .AddScoped<IImageRepository, ImageRepository>()
        .AddScoped<IInquiryRepository, InquiryRepository>()
        .AddScoped<IJobApplicantRepository, JobApplicantRepository>()
        .AddScoped<IJobFieldRepository, JobFieldRepository>()
        .AddScoped<IJobRepository, JobRepository>()
        .AddScoped<ILanguageRepository, LanguageRepository>()
        .AddScoped<INotificationRepository, NotificationRepository>()
        .AddScoped<IOneTimeTokenRepository, OneTimeTokenRepository>()
        .AddScoped<IOtpRepository, OtpRepository>()
        .AddScoped<IPaymentMethodRepository, PaymentMethodRepository>()
        .AddScoped<IPrincipalAddressRepository, PrincipalAddressRepository>()
        .AddScoped<IPrincipalBankRepository, PrincipalBankRepository>()
        .AddScoped<IPrincipalDetailItemRepository, PrincipalDetailItemRepository>()
        .AddScoped<IPrincipalDetailRepository, PrincipalDetailRepository>()
        .AddScoped<IPrincipalRepository, PrincipalRepository>()
        .AddScoped<IPrivilegeRepository, PrivilegeRepository>()
        .AddScoped<IProductAttributeRepository, ProductAttributeRepository>()
        .AddScoped<IProductImageRepository, ProductImageRepository>()
        .AddScoped<IProductRepository, ProductRepository>()
        .AddScoped<IProductReviewRepository, ProductReviewRepository>()
        .AddScoped<IPurchaseQuoteRepository, PurchaseQuoteRepository>()
        .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
        .AddScoped<IRefundRepository, RefundRepository>()
        .AddScoped<IRoleRepository, RoleRepository>()
        .AddScoped<ISaleItemRepository, SaleItemRepository>()
        .AddScoped<IShippingMethodRepository, ShippingMethodRepository>()
        .AddScoped<IStoreOrderRepository, StoreOrderRepository>()
        .AddScoped<IStoreRepository, StoreRepository>()
        .AddScoped<ISubscriberRepository, SubscriberRepository>()
        .AddScoped<ISuggestionRepository, SuggestionRepository>()
        .AddScoped<IWebPageRepository, WebPageRepository>()
        .AddScoped<IWebsiteProfileRepository, WebsiteProfileRepository>()
        .AddScoped<IWishListRepository, WishListRepository>()
        .AddScoped<IWithdrawalRepository, WithdrawalRepository>()
        .AddScoped(typeof(ISearchEngineRepository<>), typeof(SearchEngineRepository<>));

        return builder;
    }
}
