using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Messaging.Services;
using KanzApi.Resources;
using KanzApi.Transaction.Entities;
using KanzApi.Utils;
using KanzApi.Vendors.Msegat.Services;
using KanzApi.Vendors.TinyUrl.Models;
using KanzApi.Vendors.TinyUrl.Services;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace KanzApi.Account.Services;

public class NotificationFilterableService(INotificationRepository repository,
INotificationService service, ISessionService sessionService, IConfiguration configuration,
IStringLocalizer<Messages> localizer, ITinyUrlDefaultService tinyUrlDefaultService,
IMsegatSmsService msegatSmsService, IMailService mailService, ILogger<NotificationFilterableService> logger)
: FilterableCrudService<Notification, Guid?>(repository), INotificationFilterableService
{

    protected readonly INotificationService _service = service;

    protected readonly ISessionService _sessionService = sessionService;

    private readonly IConfiguration _configuration = configuration;

    private readonly IStringLocalizer<Messages> _localizer = localizer;

    private readonly ITinyUrlDefaultService _tinyUrlDefaultService = tinyUrlDefaultService;

    private readonly IMsegatSmsService _msegatSmsService = msegatSmsService;

    private readonly IMailService _mailService = mailService;

    private readonly ILogger<NotificationFilterableService> _logger = logger;

    public void Send(Principal principal, IEnumerable<Principal> approvers)
    {
        foreach (Principal approver in approvers)
        {
            Add(new()
            {
                Principal = approver,
                Title = "AccountApprovalNotificationTitle",
                Message = "AccountApprovalNotificationMessage",
                Type = ENotificationType.AccountApproval,
                ReferenceId = principal.Id.ToString()
            });
        }
    }

    public Notification Send(PurchaseQuote purchaseQuote)
    {
        Store store = purchaseQuote.Store!;
        StoreOrder order = purchaseQuote.StoreOrder!;

        Notification entity = new()
        {
            Principal = store.Principal,
            Title = ComposeTitle(purchaseQuote),
            Message = ComposeMessage(purchaseQuote),
            MessageArgs = [order.InvoiceNumber!, store.Name!,
            ((DateTime)purchaseQuote.UpdatedAt!).ToString(Constants.DateTimeFormat)],
            Image = purchaseQuote.Product?.Icon,
            Type = ENotificationType.PurchaseQuote,
            ReferenceId = order.InvoiceNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        entity = Add(entity);

        try
        {
            if (purchaseQuote.Status == EPurchaseQuoteStatus.Open)
            {
                string baseUrl = _configuration.GetValue<string>("VendorClient:BaseUrl")!;
                TinyUrlResponse<TinyUrlCreateResponse> response = _tinyUrlDefaultService
                    .Create($"{baseUrl}/en/order-management/{order.InvoiceNumber}");
                _msegatSmsService.Send(store.Principal!, _localizer.GetString("OrderOpenedSms",
                    order.InvoiceNumber!, store.Name!, purchaseQuote.UpdatedAt!, response.Data!.TinyUrl!));
            }

            _mailService.Send(purchaseQuote);
        }
        catch (Exception e)
        {
            _logger.LogError(0, e, "SMS or mail error:");
        }

        return entity;
    }

    public Notification Read(Guid id)
    {
        Notification entity = GetById(id);
        entity.ReadAt = DateTime.Now;

        return Edit(entity);
    }

    protected override Expression<Func<Notification, bool>> Filter(Expression<Func<Notification, bool>>? predicate)
    {
        Expression<Func<Notification, bool>> filterPredicate = Notification.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public PaginatedResponse<NotificationResponse> FindAllModels(NotificationPageableParam param)
    {
        PaginatedEntity<Notification> pEntity = FindAll(param);
        IEnumerable<NotificationResponse> models = pEntity.Content.Select(_service.Map).ToArray();

        return PaginatedResponse<NotificationResponse>.From(pEntity, models);
    }

    private string ComposeTitle(PurchaseQuote purchaseQuote)
    {
        return purchaseQuote.Status switch
        {
            EPurchaseQuoteStatus.Open => _localizer.GetString("OrderOpenedNotificationTitle"),
            EPurchaseQuoteStatus.Delivered => _localizer.GetString("OrderDeliveredNotificationTitle"),
            _ => ""
        };
    }

    private string ComposeMessage(PurchaseQuote purchaseQuote)
    {
        return purchaseQuote.Status switch
        {
            EPurchaseQuoteStatus.Open => _localizer
                .GetString("OrderOpenedNotificationMessage",
                    purchaseQuote.StoreOrder?.InvoiceNumber ?? "",
                    purchaseQuote.StoreOrder?.Store?.Name ?? "",
                    purchaseQuote.UpdatedAt.ToString() ?? ""),
            EPurchaseQuoteStatus.Delivered => _localizer
                .GetString("OrderDeliveredNotificationMessage",
                    purchaseQuote.StoreOrder?.InvoiceNumber ?? "",
                    purchaseQuote.StoreOrder?.Store?.Name ?? "",
                    purchaseQuote.UpdatedAt.ToString() ?? ""),
            _ => "",
        };
    }
}
