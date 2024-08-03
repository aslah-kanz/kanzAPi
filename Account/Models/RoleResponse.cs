using System.Text.Json.Serialization;

namespace KanzApi.Account.Models;

public class RoleResponse
{

    public int Id { get; set; }

    public string Name { get; set; } = "";

    [JsonPropertyName("privilegeIds")]
    public ISet<int?> Privileges { get; set; } = new HashSet<int?>();
}
