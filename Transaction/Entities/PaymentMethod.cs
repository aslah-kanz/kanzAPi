using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("PaymentMethod", Schema = "Transaction")]
public class PaymentMethod : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    [Required]
    public LocalizableString? Name { get; set; }

    public LocalizableString? Instruction { get; set; }

    public LocalizableString? Description { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    private long? _imageArId;

    public long? ImageArId { get { return _imageArId; } set { _imageArId = value; } }

    private Image? _imageAr;

    public virtual Image? ImageAr
    { get { return _imageAr; } set { _imageArId = value?.Id; _imageAr = value; } }

    public static Expression<Func<PaymentMethod, bool>> QNameContains(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.Contains(value))
        || (arg.Name.Ar != null && arg.Name.Ar.Contains(value));
    }
}
