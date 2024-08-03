using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Repositories;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Resources;
using MapsterMapper;
using Microsoft.Extensions.Localization;

namespace KanzApi.Account.Services;

public class NotificationService(INotificationRepository repository,
ISessionService sessionService, IMapper mapper, IStringLocalizer<Messages> localizer)
: CrudService<Notification, Guid?>(repository), INotificationService
{

    protected readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IStringLocalizer<Messages> _localizer = localizer;

    public NotificationResponse Map(Notification entity)
    {
        NotificationResponse model = _mapper.Map<NotificationResponse>(entity);
        model.Title = _localizer.GetString(entity.Title!);
        model.Message = entity.MessageArgs != null && entity.MessageArgs.Count != 0
        ? _localizer.GetString(entity.Message!, [.. entity.MessageArgs])
        : _localizer.GetString(entity.Message!);

        return model;
    }

    public IEnumerable<NotificationResponse>? FindAllUnreadModelsByPrincipalId(int principalId)
    {
        var notifications = FindAll(Notification.QPrincipalIdEquals(principalId)
                .And(Notification.QUnread()), new Sort("CreatedAt", EOrder.Desc))
                .Select(n => Map(n));

        return notifications;
    }
}
