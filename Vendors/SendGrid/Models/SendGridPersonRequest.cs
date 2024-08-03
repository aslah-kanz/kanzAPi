using KanzApi.Account.Entities;

namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridPersonRequest
{

    public string? Email { get; set; }

    public string? Name { get; set; }

    public static SendGridPersonRequest From(Principal entity)
    {
        return new()
        {
            Email = entity.Email,
            Name = entity.FullName
        };
    }
}
