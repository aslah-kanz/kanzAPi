using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Common.Models;
using KanzApi.Common.Validation;
using KanzApi.Product.Entities;
using KanzApi.Resources;

namespace KanzApi.Product.Models;

public class ProductRequest
{

    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Code { get; set; }

    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Mpn { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public Gtin? Gtin { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Name { get; set; }

    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Slug { get; set; }

    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? FamilyCode { get; set; }

    [JsonPropertyName("iconId")]
    public long? Icon { get; set; }

    [JsonPropertyName("imageId")]
    public long? Image { get; set; }

    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Description { get; set; }

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("brandId")]
    public int? Brand { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public double? Length { get; set; } = 0;

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public double? Width { get; set; } = 0;

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public double? Height { get; set; } = 0;

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public double? Weight { get; set; } = 0;

    [MaxSize(65, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? MetaKeyword { get; set; }

    [MaxSize(160, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? MetaDescription { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public bool? Sellable { get; set; } = true;

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public EProductStatus? Status { get; set; } = EProductStatus.Draft;

    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Comment { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("categoryIds")]
    public ISet<int>? Categories { get; set; }

    [JsonPropertyName("documentIds")]
    public ISet<int>? Documents { get; set; }

    [JsonPropertyName("imageIds")]
    public ISet<int>? ProductImages { get; set; }

    [JsonPropertyName("properties")]
    public ProductProperty? Properties { get; set; }
}


public class ProductRequestAttribute()
{
    public string Header { get; set; }
    public string Value { get; set; }
}


public class ProductProperty()
{
    public string? ProductId { get; set; }
    public string? AttributeId { get; set; }

    public ISet<ProductRequestAttribute>? Headers { get; set; }
    public ISet<ProductRequestAttribute>? Values { get; set; }

}
