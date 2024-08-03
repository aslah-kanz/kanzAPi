using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Product.Entities;

[Table("Property", Schema = "Product")]
[Index(nameof(NameEn), IsUnique = true)]
public class Property : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    [Required]
    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    private List<string>? _fieldsEn = [];

    [Required]
    public List<string>? FieldsEn { get { return _fieldsEn; } set { _fieldsEn = value; } }

    private List<string>? _fieldsAr = [];

    [Required]
    public List<string>? FieldsAr { get { return _fieldsAr; } set { _fieldsAr = value; } }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EPropertyType? Type { get; set; } = EPropertyType.Table;

    public int? SortOrder { get; set; } = 0;

    public IEnumerable<LocalizableString> ToLocalizedFields()
    {
        return _fieldsEn!.Select((e, i) => new LocalizableString()
        {
            En = e,
            Ar = _fieldsAr?.Count > i ? _fieldsAr![i] : null
        });
    }
}
