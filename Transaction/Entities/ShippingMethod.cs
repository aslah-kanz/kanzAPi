using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("ShippingMethod", Schema = "Transaction")]
public class ShippingMethod : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    [Required]
    [MaxLength(100)]
    public string? ProviderName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? DeliveryCompanyName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? DeliveryEstimateTime { get; set; }

    public string? Detail { get; set; }

    public static Expression<Func<ShippingMethod, bool>> QNameContains(string value)
    {
        return arg => arg.DeliveryCompanyName!.Contains(value);
    }

}
