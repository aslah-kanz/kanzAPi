using AttributeEntity = KanzApi.Product.Entities.Attribute;
using CustomerOrderItemEntity = KanzApi.Transaction.Entities.CustomerOrderItem;
using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Security.Entities;
using KanzApi.Transaction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Configurations.Contexts;

public class CommonDbContext(DbContextOptions<CommonDbContext> options,
IConfiguration configuration, ISessionService sessionService)
: DbContext(options)
{

    private readonly IConfiguration _configuration = configuration;

    private readonly ISessionService _sessionService = sessionService;

    public DbSet<AttributeEntity> Attributes { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartSaleItem> CartSaleItems { get; set; }
    public DbSet<Catalogue> Catalogues { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<CustomerOrderItemEntity> CustomerOrderItems { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<Faq> Faqs { get; set; }
    public DbSet<FaqGroup> FaqGroups { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobApplicant> JobApplicants { get; set; }
    public DbSet<JobField> JobFields { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<OneTimeToken> OneTimeTokens { get; set; }
    public DbSet<Otp> Otps { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Principal> Principals { get; set; }
    public DbSet<PrincipalAddress> PrincipalAddresses { get; set; }
    public DbSet<PrincipalBank> PrincipalBanks { get; set; }
    public DbSet<PrincipalDetail> PrincipalDetails { get; set; }
    public DbSet<PrincipalDetailItem> PrincipalDetailItems { get; set; }
    public DbSet<PrincipalWallet> PrincipalWallets { get; set; }
    public DbSet<Privilege> Privileges { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PurchaseQuote> PurchaseQuotes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    public DbSet<Shipping> Shippings { get; set; }
    public DbSet<ShippingMethod> ShippingMethods { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<StoreOrder> StoreOrders { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<WebPage> WebPages { get; set; }
    public DbSet<WebsiteProfile> WebsiteProfiles { get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<Withdraw> Withdraws { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
        .UseSqlServer(_configuration.GetValue<string>("KanzApi:Db:ConnectionString"))
        .UseLazyLoadingProxies(true);

        base.OnConfiguring(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Conventions.Add(_ => new BlankTriggerAddingConvention());
    }

    public override int SaveChanges()
    {
        DateTime now = DateTime.Now;
        int auditorId = _sessionService.CurrentAuditorId();

        foreach (EntityEntry<ILoggable> entry in ChangeTracker.Entries<ILoggable>())
        {
            ILoggable entity = entry.Entity;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = auditorId;
            }
            else if (entry.State != EntityState.Modified) continue;

            if (entity is IAuditable aEntity)
            {
                aEntity.UpdatedAt = now;
                aEntity.UpdatedBy = auditorId;
            }
        }

        return base.SaveChanges();
    }

    private void BindOneToManys(ModelBuilder builder)
    {
        builder.Entity<Cart>().BindOneToMany(e => e.Product);
        builder.Entity<CartSaleItem>().BindOneToMany(e => e.Product);
        builder.Entity<CartSaleItem>().BindOneToMany(e => e.Store);
        builder.Entity<CustomerOrder>().BindOneToMany(e => e.Address);
        builder.Entity<CustomerOrder>().BindOneToMany(e => e.HiglightedProduct);
        builder.Entity<CustomerOrder>().BindOneToMany(e => e.PaymentMethod);
        builder.Entity<CustomerOrderItemEntity>().BindOneToMany(e => e.Product);
        builder.Entity<Exchange>().BindOneToMany(e => e.Principal);
        builder.Entity<Exchange>().BindOneToMany(e => e.PurchaseQuote);
        builder.Entity<Inquiry>().BindOneToMany(e => e.Product);
        builder.Entity<Notification>().BindOneToMany(e => e.Principal);
        builder.Entity<PrincipalDetail>().BindOneToMany(e => e.Country);
        builder.Entity<ProductAttribute>().BindOneToMany(e => e.Attribute);
        builder.Entity<ProductAttribute>().BindOneToMany(e => e.Property);
        builder.Entity<ProductEntity>().BindOneToMany(e => e.Brand);
        builder.Entity<ProductReview>().BindOneToMany(e => e.Principal);
        builder.Entity<ProductReview>().BindOneToMany(e => e.PurchaseQuote);
        builder.Entity<PurchaseQuote>().BindOneToMany(e => e.CustomerOrder);
        builder.Entity<PurchaseQuote>().BindOneToMany(e => e.Product);
        builder.Entity<PurchaseQuote>().BindOneToMany(e => e.Store);
        builder.Entity<PurchaseQuote>().BindOneToMany(e => e.StoreOrder);
        builder.Entity<Refund>().BindOneToMany(e => e.Principal);
        builder.Entity<Refund>().BindOneToMany(e => e.PurchaseQuote);
        builder.Entity<SaleItem>().BindOneToMany(e => e.Store);
        builder.Entity<Shipping>().BindOneToMany(e => e.ShippingMethod);
        builder.Entity<Store>().BindOneToMany(e => e.Principal);
        builder.Entity<StoreOrder>().BindOneToMany(e => e.Store);
        builder.Entity<WishList>().BindOneToMany(e => e.Principal);
        builder.Entity<WishList>().BindOneToMany(e => e.Product);
    }

    private void BindManyToManys(ModelBuilder builder)
    {
        builder.BindManyToMany<Brand, Category, BrandCategory>(e => e.Categories, e => e.Brands);
        builder.BindManyToMany<Exchange, Image, ExchangeImage>(e => e.Images);
        builder.BindManyToMany<Principal, Department, PrincipalDepartment>(e => e.Departments, e => e.Principals);
        builder.BindManyToMany<Principal, PrincipalDetail, CompanyMember>(e => e.PrincipalDetails, e => e.Principals);
        builder.BindManyToMany<Principal, Role, PrincipalRole>(e => e.Roles, e => e.Principals);
        builder.BindManyToMany<Principal, Store, PrincipalStore>(e => e.Stores, e => e.Principals);
        builder.BindManyToMany<ProductEntity, Category, ProductCategory>(e => e.Categories, e => e.Products);
        builder.BindManyToMany<ProductReview, Image, ProductReviewImage>(e => e.Images);
        builder.BindManyToMany<Refund, Image, RefundImage>(e => e.Images);
        builder.BindManyToMany<Role, Privilege, RolePrivilege>(e => e.Privileges, e => e.Roles);
    }

    private void BindJsons(ModelBuilder builder)
    {
        builder.Entity<Banner>()
        .BindJson(e => e.Title)
        .BindJson(e => e.Description);

        builder.Entity<Blog>()
        .BindJson(e => e.Title)
        .BindJson(e => e.Description);

        builder.Entity<Brand>()
        .BindJson(e => e.Name)
        .BindJson(e => e.Description);

        builder.Entity<Catalogue>()
        .BindJson(e => e.Title)
        .BindJson(e => e.Description);

        builder.Entity<Category>()
        .BindJson(e => e.Name)
        .BindJson(e => e.Description);

        builder.Entity<Certificate>()
        .BindJson(e => e.Title);

        builder.Entity<CustomerOrder>()
        .BindJsonArray(e => e.DeliveryOptions);

        builder.Entity<Country>()
        .BindJson(e => e.Name);

        builder.Entity<Faq>()
        .BindJson(e => e.Answer)
        .BindJson(e => e.Question);

        builder.Entity<FaqGroup>()
        .BindJson(e => e.Title)
        .BindJson(e => e.Description);

        builder.Entity<Job>()
        .BindJson(e => e.Title)
        .BindJson(e => e.Responsibility)
        .BindJson(e => e.JobType)
        .BindJson(e => e.Experience)
        .BindJson(e => e.JobLocation);

        builder.Entity<JobField>()
        .BindJson(e => e.Name);

        builder.Entity<Language>()
        .BindJson(e => e.Name);

        builder.Entity<Notification>()
        .BindJsonArray(e => e.MessageArgs);

        builder.Entity<PaymentMethod>()
        .BindJson(e => e.Name)
        .BindJson(e => e.Instruction)
        .BindJson(e => e.Description);

        builder.Entity<ProductEntity>()
        .BindJson(e => e.Gtin)
        .BindJson(e => e.Name)
        .BindJson(e => e.Description);

        builder.Entity<Property>()
        .BindJsonArray(e => e.FieldsEn)
        .BindJsonArray(e => e.FieldsAr);

        builder.Entity<WebPage>()
        .BindJson(e => e.Title)
        .BindJsons(e => e.Contents);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        BindOneToManys(builder);
        BindManyToManys(builder);
        BindJsons(builder);
    }
}
