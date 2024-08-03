using KanzApi.Account.Services;
using KanzApi.Common.Services;
using KanzApi.Messaging.Services;
using KanzApi.Product.Services;
using KanzApi.Security.Services;
using KanzApi.Transaction.Services;
using KanzApi.Vendors.Azure.Services;
using KanzApi.Vendors.Graph.Services;
using KanzApi.Vendors.Msegat.Services;
using KanzApi.Vendors.Oto.Services;
using KanzApi.Vendors.SendGrid.Services;
using KanzApi.Vendors.TinyUrl.Services;
using KanzApi.Vendors.Urway.Services;

namespace KanzApi.Configurations;

public static class ServiceConfig
{

    public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;

        services
        .AddHttpContextAccessor()
        .AddSingleton<IAzureStorageService, AzureStorageService>()
        .AddSingleton<ICodeGenerator, CodeGenerator>()
        .AddSingleton<IErrorResponseProvider, ErrorResponseProvider>()
        .AddSingleton<IGraphMailService, GraphMailService>()
        .AddSingleton<IJwtTokenService, JwtTokenService>()
        .AddSingleton<IMsegatOtpService, MsegatOtpService>()
        .AddSingleton<IMsegatSmsService, MsegatSmsService>()
        .AddSingleton<IOtoAuthorizationService, OtoAuthorizationService>()
        .AddSingleton<IOtoAwbPrinterService, OtoAwbPrinterService>()
        .AddSingleton<IOtoCheckoutService, OtoCheckoutService>()
        .AddSingleton<IOtoOrderService, OtoOrderService>()
        .AddSingleton<IOtoPickupLocationService, OtoPickupLocationService>()
        .AddSingleton<IPasswordService, PasswordService>()
        .AddSingleton<ISendGridMailService, SendGridMailService>()
        .AddSingleton<ISessionService, SessionService>()
        .AddSingleton<ITinyUrlDefaultService, TinyUrlDefaultService>()
        .AddSingleton<IUrwayTransactionService, UrwayTransactionService>()
        .AddScoped<IAdminOrderService, AdminOrderService>()
        .AddScoped<IAttributeService, AttributeService>()
        .AddScoped<IAuthService, AuthService>()
        .AddScoped<IBannerService, BannerService>()
        .AddScoped<IBlogService, BlogService>()
        .AddScoped<IBrandService, BrandService>()
        .AddScoped<ICartFilterableService, CartFilterableService>()
        .AddScoped<ICartService, CartService>()
        .AddScoped<ICatalogueService, CatalogueService>()
        .AddScoped<ICategoryService, CategoryService>()
        .AddScoped<ICertificateService, CertificateService>()
        .AddScoped<ICountryService, CountryService>()
        .AddScoped<ICurrencyService, CurrencyService>()
        .AddScoped<ICustomerOrderActionService, CustomerOrderActionService>()
        .AddScoped<ICustomerOrderFilterableService, CustomerOrderFilterableService>()
        .AddScoped<ICustomerOrderItemService, CustomerOrderItemService>()
        .AddScoped<ICustomerOrderPaymentService, CustomerOrderPaymentService>()
        .AddScoped<ICustomerOrderService, CustomerOrderService>()
        .AddScoped<ICustomerProfileService, CustomerProfileService>()
        .AddScoped<IDashboardService, DashboardService>()
        .AddScoped<IDepartmentFilterableService, DepartmentFilterableService>()
        .AddScoped<IDocumentService, DocumentService>()
        .AddScoped<IExchangeFilterableService, ExchangeFilterableService>()
        .AddScoped<IExchangeService, ExchangeService>()
        .AddScoped<IFaqGroupService, FaqGroupService>()
        .AddScoped<IFaqService, FaqService>()
        .AddScoped<IImageService, ImageService>()
        .AddScoped<IInquiryFilterableService, InquiryFilterableService>()
        .AddScoped<IJobApplicantService, JobApplicantService>()
        .AddScoped<IJobFieldService, JobFieldService>()
        .AddScoped<IJobService, JobService>()
        .AddScoped<ILanguageService, LanguageService>()
        .AddScoped<IMailService, MailService>()
        .AddScoped<INotificationFilterableService, NotificationFilterableService>()
        .AddScoped<INotificationService, NotificationService>()
        .AddScoped<IOneTimeTokenService, OneTimeTokenService>()
        .AddScoped<IOtpService, OtpService>()
        .AddScoped<IPaymentMethodService, PaymentMethodService>()
        .AddScoped<IPrincipalAddressFilterableService, PrincipalAddressFilterableService>()
        .AddScoped<IPrincipalAddressService, PrincipalAddressService>()
        .AddScoped<IPrincipalBankFilterableService, PrincipalBankFilterableService>()
        .AddScoped<IPrincipalDetailFilterableService, PrincipalDetailFilterableService>()
        .AddScoped<IPrincipalDetailItemService, PrincipalDetailItemService>()
        .AddScoped<IPrincipalService, PrincipalService>()
        .AddScoped<IPrivilegeService, PrivilegeService>()
        .AddScoped<IProductAttributeService, ProductAttributeService>()
        .AddScoped<IProductImageService, ProductImageService>()
        .AddScoped<IProductReviewFilterableService, ProductReviewFilterableService>()
        .AddScoped<IProductReviewService, ProductReviewService>()
        .AddScoped<IProductSaleItemFilterableService, ProductSaleItemFilterableService>()
        .AddScoped<IProductSyncableService, ProductSyncableService>()
        .AddScoped<IPurchaseQuoteActionService, PurchaseQuoteActionService>()
        .AddScoped<IPurchaseQuoteFilterableService, PurchaseQuoteFilterableService>()
        .AddScoped<IPurchaseQuoteService, PurchaseQuoteService>()
        .AddScoped<IRefreshTokenService, RefreshTokenService>()
        .AddScoped<IRefundFilterableService, RefundFilterableService>()
        .AddScoped<IRoleService, RoleService>()
        .AddScoped<ISaleItemFilterableService, SaleItemFilterableService>()
        .AddScoped<ISaleItemSellableService, SaleItemSellableService>()
        .AddScoped<ISaleItemSyncableService, SaleItemSyncableService>()
        .AddScoped<ISaleProductFilterableService, SaleProductFilterableService>()
        .AddScoped<ISaleProductService, SaleProductService>()
        .AddScoped<IShippingMethodService, ShippingMethodService>()
        .AddScoped<IStoreActionService, StoreActionService>()
        .AddScoped<IStoreFilterableService, StoreFilterableService>()
        .AddScoped<IStoreOrderFilterableService, StoreOrderFilterableService>()
        .AddScoped<IStoreOrderService, StoreOrderService>()
        .AddScoped<IStoreService, StoreService>()
        .AddScoped<ISubscriberService, SubscriberService>()
        .AddScoped<ISuggestionService, SuggestionService>()
        .AddScoped<ITokenService, TokenService>()
        .AddScoped<IWebPageService, WebPageService>()
        .AddScoped<IWebsiteProfileService, WebsiteProfileService>()
        .AddScoped<IWishListFilterableService, WishListFilterableService>()
        .AddScoped<IWithdrawalFilterableService, WithdrawalFilterableService>()
        .AddScoped<IWithdrawalService, WithdrawalService>()
        .AddScoped(typeof(ISearchEngineService<>), typeof(SearchEngineService<>));

        return builder;
    }
}
