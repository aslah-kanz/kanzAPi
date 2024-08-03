using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Product.Entities;

[Table("Attribute", Schema = "Product")]
[Index(nameof(GroupEn), nameof(NameEn), nameof(Unit1En), nameof(Unit2En), nameof(Unit3En), IsUnique = true)]
public class Attribute : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    [Required]
    public string? GroupEn { get; set; }

    public string? GroupAr { get; set; }

    [Required]
    public int? GroupOrder { get; set; } = 0;

    [Required]
    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? Unit1En { get; set; }

    public string? Unit1Ar { get; set; }

    public string? Unit2En { get; set; }

    public string? Unit2Ar { get; set; }

    public string? Unit3En { get; set; }

    public string? Unit3Ar { get; set; }

    [Required]
    public int? SortOrder { get; set; } = 0;
}
