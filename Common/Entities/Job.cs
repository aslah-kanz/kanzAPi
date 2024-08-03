using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Entities;

[Table("Job", Schema = "Common")]
public class Job : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    public LocalizableString? Title { get; set; }

    [MaxLength(100)]
    public string? Slug { get; set; }

    [Required]
    public LocalizableString? Responsibility { get; set; }

    [MaxLength(255)]
    public string? Requirement { get; set; }

    [Required]
    public LocalizableString? JobType { get; set; }

    [Required]
    public LocalizableString? Experience { get; set; }

    [Required]
    public LocalizableString? JobLocation { get; set; }

    private int? _jobFieldId;

    [Required]
    public int? JobFieldId { get { return _jobFieldId; } set { _jobFieldId = value; } }

    private JobField? _jobField;

    public virtual JobField? JobField
    {
        get { return _jobField; }
        set
        {
            if (value != null) _jobFieldId = value.Id;
            _jobField = value;
        }
    }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;

    public static Expression<Func<Job, bool>> QTitleContains(string value)
    {
        return arg
        => (arg.Title!.En != null && arg.Title.En.Contains(value))
        || (arg.Title.Ar != null && arg.Title.Ar.Contains(value));
    }

    public static Expression<Func<Job, bool>> QJobFieldIdEquals(int value)
    {
        return arg => arg.JobFieldId == value;
    }
}
