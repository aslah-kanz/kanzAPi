using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Transaction.Entities;

[Table("ExchangeImage", Schema = "Transaction")]
public class ExchangeImage
{

    public Guid? ExchangeId { get; set; }

    public long? ImageId { get; set; }
}
