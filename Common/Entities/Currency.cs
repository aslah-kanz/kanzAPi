using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Entities;

[Table("Currency", Schema = "Common")]
[Index(nameof(Code), IsUnique = true)]
public class Currency : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string? Code { get; set; }

    [Required]
    [MaxLength(125)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(125)]
    public string? Country { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Symbol { get; set; }

    public static Expression<Func<Currency, bool>> QCodeEquals(string value)
    {
        return arg => arg.Code!.Equals(value);
    }

    public static Expression<Func<Currency, bool>> QCountryEquals(string value)
    {
        return arg => arg.Country!.Equals(value);
    }

    public static Expression<Func<Currency, bool>> QDescriptionContains(string value)
    {
        return arg => arg.Description!.Contains(value);
    }
}
