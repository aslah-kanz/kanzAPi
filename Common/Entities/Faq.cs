using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Entities;

[Table("Faq", Schema = "Common")]
public class Faq : CommonEntity<int?>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    private int? _faqGroupId;

    [Required]
    public int? FaqGroupId { get { return _faqGroupId; } set { _faqGroupId = value; } }

    private FaqGroup? _faqGroup;

    public virtual FaqGroup? FaqGroup
    { get { return _faqGroup; } set { _faqGroupId = value?.Id; _faqGroup = value; } }

    [Required]
    public LocalizableString? Question { get; set; }

    public LocalizableString? Answer { get; set; }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;

    public static Expression<Func<Faq, bool>> QGroupIdEquals(int value)
    {
        return arg => arg.FaqGroupId == value;
    }

    public static Expression<Func<Faq, bool>> QQuestionContains(string value)
    {
        return arg
        => (arg.Question!.En != null && arg.Question.En.Contains(value))
        || (arg.Question.Ar != null && arg.Question.Ar.Contains(value));
    }
}
