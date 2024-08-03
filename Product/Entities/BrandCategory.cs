using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Product.Entities;

[Table("BrandCategory", Schema = "Product")]
public class BrandCategory
{

    public int? BrandId { get; set; }

    public int? CategoryId { get; set; }
}
