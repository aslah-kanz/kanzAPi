using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Transaction.Entities;

[Table("RefundImage", Schema = "Transaction")]
public class RefundImage
{

    public Guid? RefundId { get; set; }

    public long? ImageId { get; set; }
}
