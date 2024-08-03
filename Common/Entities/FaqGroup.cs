using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Entities;

[Table("FaqGroup", Schema = "Common")]
public class FaqGroup : CommonEntity<int?>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    public LocalizableString? Title { get; set; }

    public LocalizableString? Description { get; set; }

    [Required]
    public bool? ShowAtHomePage { get; set; } = false;

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;

    public virtual ISet<Faq> Faqs { get; set; } = new HashSet<Faq>();

    public static Expression<Func<FaqGroup, bool>> QTitleContains(string value)
    {
        return arg
        => (arg.Title!.En != null && arg.Title.En.Contains(value))
        || (arg.Title.Ar != null && arg.Title.Ar.Contains(value));
    }

    public static Expression<Func<FaqGroup, bool>> QShowAtHomePageEquals(bool value)
    {
        return arg => arg.ShowAtHomePage == value;
    }
}
