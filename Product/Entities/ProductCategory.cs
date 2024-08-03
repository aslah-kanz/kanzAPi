using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Product.Entities;

[Table("ProductCategory", Schema = "Product")]
public class ProductCategory
{

    public int? ProductId { get; set; }

    public int? CategoryId { get; set; }
}
