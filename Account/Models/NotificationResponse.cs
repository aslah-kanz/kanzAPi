using KanzApi.Account.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Account.Models;

public class NotificationResponse
{

    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    public string Message { get; set; } = "";

    public ImageResponse? Image { get; set; }

    public ENotificationType? Type { get; set; }

    public string? ReferenceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }
}
