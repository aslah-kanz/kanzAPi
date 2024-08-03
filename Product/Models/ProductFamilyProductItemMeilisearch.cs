using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class ProductFamilyProductItemMeilisearch
{
	public required string Id { get; set; } // PRIMARY KEY

	public string? FamilyCode { get; set; }

	public List<string> NamesEn { get; set; } = [];

	public List<string> NamesAr { get; set; } = [];

	public LocalizableString? Brand { get; set; } = new();

	public List<string>? Mpns { get; set; }

	public List<string>? Categories { get; set; }

	public Dictionary<string, PriceSummary>? PriceSummaries { get; set; }

	public Dictionary<string, List<string>>? SpecificationValuesEn { get; set; }

	public Dictionary<string, List<string>>? SpecificationValuesAr { get; set; }
}

public class PriceSummary
{
	public string? Country { get; set; }

	public PriceCountry Price { get; set; } = new();

	public bool IsAvailable { get; set; }

}

public class PriceCountry
{

	public LocalizableString? Symbol { get; set; }

	public decimal? LowestPrice { get; set; }
	
	public decimal? OriginalPrice { get; set; }

	public decimal? HighestPrice { get; set; }

}

public class ProductMeilisearch
{

	public int Id { get; set; }

	public string? FamilyCode { get; set; }

	public string? Category { get; set; }

	public string? Mpn { get; set; }

	public Gtin? Gtin { get; set; }

	public LocalizableString Name { get; set; } = new();

	public string? Slug { get; set; }

	public ImageResponse? Icon { get; set; }

	public ImageResponse? Image { get; set; }

	public LocalizableString? Brand { get; set; } = new();

	public Dictionary<string, PriceSummary>? PriceSummaries { get; set; }

	public string? Status { get; set; }

	public Dictionary<string, string>? SpecificationValuesEn { get; set; }

	public Dictionary<string, string>? SpecificationValuesAr { get; set; }

	public IEnumerable<SaleItemMeilisearch>? SaleItems { get; set; }

}
