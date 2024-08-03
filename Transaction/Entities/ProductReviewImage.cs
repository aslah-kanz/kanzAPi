using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Transaction.Entities;

[Table("ProductReviewImage", Schema = "Transaction")]
public class ProductReviewImage
{

    public Guid? ProductReviewId { get; set; }

    public long? ImageId { get; set; }
}
