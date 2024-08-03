using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Common.Entities;

[Table("JobApplicant", Schema = "Common")]
public class JobApplicant : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    private int? _jobId;

    [Required]
    public int? JobId { get { return _jobId; } set { _jobId = value; } }

    private Job? _job;

    public virtual Job? Job
    { get { return _job; } set { _jobId = value?.Id; _job = value; } }

    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(65)]
    public string? PhoneNumber { get; set; }

    private long? _documentId;

    public long? DocumentId { get { return _documentId; } set { _documentId = value; } }

    private Document? _document;

    public virtual Document? Document
    { get { return _document; } set { _documentId = value?.Id; _document = value; } }

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EJobApplicantStatus? Status { get; set; } = EJobApplicantStatus.WaitingReview;

    public static Expression<Func<JobApplicant, bool>> QNameContains(string value)
    {
        return arg => arg.Name!.Contains(value);
    }
    public static Expression<Func<JobApplicant, bool>> QJobIdEquals(int value)
    {
        return arg => arg.JobId == value;
    }
}
