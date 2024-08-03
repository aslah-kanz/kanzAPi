using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Repositories;
using KanzApi.Utils;
using Meilisearch;

namespace KanzApi.Product.Services;

public class SearchEngineService<T>(ISearchEngineRepository<ProductFamilyProductItemMeilisearch> searchEngineFamilyRepository, ISearchEngineRepository<ProductMeilisearch> searchEngineProductRepository,
	IPrincipalService principalService, ISaleProductFilterableService saleProductService)
	: ISearchEngineService<ProductFamilyProductItemMeilisearch>
{
	private readonly ISearchEngineRepository<ProductFamilyProductItemMeilisearch> _searchEngineFamilyRepository = searchEngineFamilyRepository;

	private readonly ISearchEngineRepository<ProductMeilisearch> _searchEngineProductRepository = searchEngineProductRepository;

	private readonly IPrincipalService _principalService = principalService;

	private readonly ISaleProductFilterableService _saleProductService = saleProductService;

	public async Task<TaskInfoStatus> SyncProducts()
	{
		// ========================================================
		// TODO: Uncomment principal check after development phase
		// ========================================================

		//Principal principal = _principalService.GetCurrent(); 
		//if (principal != null || principal?.Type == EPrincipalType.Admin)
		{
			var familyPageIndex = 0;
			var familyPageSize = 36; // TODO: change to optimal value
			var pageIndex = 1;
			var pageSize = 100; // TODO: change to optimal value
			var repeatFamilies = true;

			while (repeatFamilies)
			{
				// For Testing
				//if (familyPageIndex == 1) repeatFamilies = false;

				// Get Families
				var families = _saleProductService.FindAllFamilies(new ProductFamilyPageableParam() { Page = familyPageIndex, Size = familyPageSize });
				if (!families.Content.Any()) repeatFamilies = false;

				foreach (var family in families.Content)
				{
					ProductFamilyProductItemMeilisearch familyCode = new()
					{
						Id = GuidBase62.GenerateGuidByString(family).ToString(),
						FamilyCode = family
					};

					List<ProductMeilisearch> products = [];

					var repeatModels = true;
					while (repeatModels)
					{
						// Get Product Model
						var models = _saleProductService.FindAllSearchablesByFamilyCode(new SaleProductPageableParam() { Page = pageIndex, Size = pageSize }, family);
						products.AddRange(models);

						if (!models.Any() || models.Count() < pageSize)
						{
							pageIndex = 0;
							repeatModels = false;
							break;
						}
						pageIndex++;
					}

					familyCode.Mpns = products.Where(x => !string.IsNullOrEmpty(x.Mpn)).Select(p => p.Mpn).ToList()!;
					familyCode.Categories = products.FirstOrDefault()?.Category != null ? [.. products.FirstOrDefault()?.Category?.Trim().Split('>')] : [];
					familyCode.NamesEn = products.Select(x => x.Name.En).ToList()!;
					familyCode.NamesAr = products.Where(x => x.Name.Ar != null).Select(x => x.Name.Ar).ToList()!;
					familyCode.Brand = products.FirstOrDefault()?.Brand;
					familyCode.PriceSummaries = GetPriceSummaries(products);
					familyCode.SpecificationValuesEn = products.Where(x => x.SpecificationValuesEn != null).SelectMany(x => x.SpecificationValuesEn).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Select(i => i.Value).ToList());
					familyCode.SpecificationValuesAr = products.Where(x => x.SpecificationValuesAr != null).SelectMany(x => x.SpecificationValuesAr).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Select(i => i.Value).ToList());

					// Insert FamilyCode into Meilisearch
					await _searchEngineFamilyRepository.AddOrReplace([familyCode]);
					//// Insert Product into Meilisearch
					await _searchEngineProductRepository.AddOrReplace(products);
				}
				familyPageIndex++;
			}

			await _searchEngineFamilyRepository.FilterableSettings();
			await _searchEngineProductRepository.FilterableSettings();
			await _searchEngineFamilyRepository.SearchableSettings();
			await _searchEngineProductRepository.SearchableSettings();
			await _searchEngineFamilyRepository.SortableSettings();
			await _searchEngineProductRepository.SortableSettings();

			return TaskInfoStatus.Succeeded;
		}
		//else
		//{
		//	throw new PrincipalNotAllowedException();
		//}
	}

	public async Task<TaskInfoStatus> AddOrReplace(IEnumerable<ProductFamilyProductItemMeilisearch> documents)
	{
		return await _searchEngineFamilyRepository.AddOrReplace(documents);
	}

	public async Task<TaskInfoStatus> Update(IEnumerable<ProductFamilyProductItemMeilisearch> document)
	{
		return await _searchEngineFamilyRepository.Update(document);
	}

	public async Task<IReadOnlyCollection<ProductFamilyProductItemMeilisearch>> FindDocumentByFamilyCode(string fieldName, string value)
	{
		return await _searchEngineFamilyRepository.FindDocumentByFamilyCode(value);
	}

	public async Task<TaskInfoStatus> UpdateDocumentProductStatus(string value, int id, EProductStatus status)
	{
		var document = await _searchEngineFamilyRepository.FindDocumentByFamilyCode(value);
		var findDocument = document.FirstOrDefault();
		if (findDocument != null)
		{
			//if (findDocument.Products != null && findDocument.Products.Count > 0)
			//{
			//	var product = findDocument.Products.FirstOrDefault(x => x.Id == id);
			//	if (product != null)
			//	{
			//		product.Status = status.ToString();
			//		return _searchEngineRepository.Update([findDocument]).GetAwaiter().GetResult();
			//	}
			//}
		}
		return TaskInfoStatus.Failed;
	}

	public TaskInfoStatus SyncProductsByFamilyCode(string code, int? deletedId = null)
	{
		var pageIndex = 1;
		var pageSize = 100; // TODO: change to optimal value

		var repeatModels = true;

		ProductFamilyProductItemMeilisearch familyCode = new()
		{
			Id = GuidBase62.GenerateGuidByString(code).ToString(),
			FamilyCode = code
		};

		List<ProductMeilisearch> products = [];

		while (repeatModels)
		{
			// Get Product Model
			var models = _saleProductService.FindAllSearchablesByFamilyCode(new SaleProductPageableParam() { Page = pageIndex, Size = pageSize }, code);
			products.AddRange(models);
			if (models.Count == 0 || models.Count < pageSize)
			{
				repeatModels = false;
				pageIndex = 0;
				break;
			}
			pageIndex++;
		}

		if (products.Count != 0)
		{
			familyCode.Mpns = products.Where(x => !string.IsNullOrEmpty(x.Mpn)).Select(p => p.Mpn).ToList()!;
			familyCode.Categories = products.FirstOrDefault()?.Category != null ? [.. products.FirstOrDefault()?.Category?.Trim().Split('>')] : [];
			familyCode.NamesEn = products.Select(x => x.Name.En).ToList()!;
			familyCode.NamesAr = products.Where(x => x.Name.Ar != null).Select(x => x.Name.Ar).ToList()!;
			familyCode.Brand = products.FirstOrDefault()?.Brand;
			familyCode.PriceSummaries = GetPriceSummaries(products);
			familyCode.SpecificationValuesEn = products.Where(x => x.SpecificationValuesEn != null).SelectMany(x => x.SpecificationValuesEn).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Select(i => i.Value).ToList());
			familyCode.SpecificationValuesAr = products.Where(x => x.SpecificationValuesAr != null).SelectMany(x => x.SpecificationValuesAr).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Select(i => i.Value).ToList());

			// Sync into Meilisearch
			_searchEngineFamilyRepository.AddOrReplace([familyCode]).GetAwaiter().GetResult();
			// Insert FamilyCode into Meilisearch
			_searchEngineProductRepository.AddOrReplace(products).GetAwaiter().GetResult();
		}
		else if (products.Count == 0)
		{
			// We should delete the families here since the product was deleted
			_searchEngineFamilyRepository.Delete([GuidBase62.GenerateGuidByString(code).ToString()]);
			if (deletedId.HasValue)
			{
				_searchEngineProductRepository.Delete([deletedId.Value.ToString()]);
			}
		}

		return TaskInfoStatus.Succeeded;
	}

	public async Task<TaskInfoStatus> SyncProductsById(List<int> ids)
	{
		List<ProductMeilisearch> products = [];

		foreach (int id in ids)
		{
			var product = _saleProductService.FindAllSearchablesById(new SaleProductPageableParam() { Page = 0, Size = 10 }, id);
			products.AddRange(product);
		}

		// Insert FamilyCode into Meilisearch
		TaskInfoStatus status = await _searchEngineProductRepository.AddOrReplace(products);

		return status;
	}

	public async Task<TaskInfoStatus> Delete(IEnumerable<string> ids)
	{
		return await _searchEngineFamilyRepository.Delete(ids);
	}

	public async Task<TaskInfoStatus> FilterableSettings(string indexName)
	{
		if (indexName == "products")
		{
			return await _searchEngineProductRepository.FilterableSettings();
		}

		return await _searchEngineFamilyRepository.FilterableSettings();
	}

	public async Task<TaskInfoStatus> SearchableSettings(string indexName)
	{
		if (indexName == "products")
		{
			return await _searchEngineProductRepository.SearchableSettings();
		}

		return await _searchEngineFamilyRepository.SearchableSettings();
	}

	public async Task<TaskInfoStatus> SortableSettings(string indexName)
	{
		if (indexName == "products")
		{
			return await _searchEngineProductRepository.SortableSettings();
		}

		return await _searchEngineFamilyRepository.SortableSettings();
	}

	public async Task<TaskInfoStatus> FacetedSettings(string indexName)
	{
		if (indexName == "products")
		{
			return await _searchEngineProductRepository.FacetedSettings();
		}

		return await _searchEngineFamilyRepository.FacetedSettings();
	}

	private static Dictionary<string, PriceSummary> GetPriceSummaries(List<ProductMeilisearch> products)
	{
		Dictionary<string, PriceSummary> priceSummaries = [];
		string[] countryNames = ["SA", "QA", "AE", "ID", "MY"];

		foreach (var country in countryNames)
		{
			decimal lowestPrice = GetMinPriceRange(products, country) ?? 0;
			decimal? oriPrice = GetOriPriceRange(products, country);
			decimal? highestPrice = GetMaxPriceRange(products, country);
			PriceSummary priceSummary = new()
			{
				Country = country,
				Price = new()
				{
					Symbol = GetCountrySymbol(country),
					LowestPrice = lowestPrice,
					OriginalPrice = lowestPrice == oriPrice ? null : oriPrice,
					HighestPrice = lowestPrice == highestPrice ? null : highestPrice
				},
				IsAvailable = products.Select(x => x.SaleItems != null && x.SaleItems.Any()) != null && lowestPrice != 0
			};
			priceSummaries.Add(country, priceSummary);
		}

		return priceSummaries;
	}

	private static LocalizableString GetCountrySymbol(string country)
	{
		return country switch
		{
			"SA" => new LocalizableString()
			{
				Ar = "ر.س",
				En = "SAR"
			},
			"QA" => new LocalizableString()
			{
				Ar = "ر.ق",
				En = "QAR"
			},
			"AE" => new LocalizableString()
			{
				Ar = "در",
				En = "AED"
			},
			"ID" => new LocalizableString()
			{
				Ar = "ر.ب",
				En = "RP"
			},
			"MY" => new LocalizableString()
			{
				Ar = "ر.م",
				En = "RM"
			},
			_ => new LocalizableString(),
		};
	}

	private static decimal? GetMinPriceRange(List<ProductMeilisearch> products, string country)
	{
		// get only available saleIteam
		var productsSaleItemsAvailable = products.Where(x => x.SaleItems.Any()).Select(s => s.SaleItems);
		// Get SalesItems
		var saleItems = productsSaleItemsAvailable.Select(x =>
		{
			var asd = x.Where(x => x.Country == country &&
			x.Status == Common.Entities.EActivableStatus.Active &&
			x.Enabled.HasValue && x.Enabled.Value);
			var minPrices = asd.Select(x => x.Price);
			return minPrices.Min();
		});

		return saleItems.Min();
	}

	private static decimal? GetOriPriceRange(List<ProductMeilisearch> products, string country)
	{
		// get only available saleIteam
		var productsSaleItemsAvailable = products.Where(x => x.SaleItems.Any()).Select(s => s.SaleItems);
		// Get SalesItems
		var saleItems = productsSaleItemsAvailable.Select(x =>
		{
			var asd = x.Where(x => x.Country == country &&
			x.Status == Common.Entities.EActivableStatus.Active &&
			x.Enabled.HasValue && x.Enabled.Value);
			var minPrices = asd.Select(x => x.OriginalPrice);
			return minPrices.Min();
		});

		return saleItems.Min();
	}

	private static decimal? GetMaxPriceRange(List<ProductMeilisearch> products, string country)
	{
		// get only available saleIteam
		var productsSaleItemsAvailable = products.Where(x => x.SaleItems.Any()).Select(s => s.SaleItems);
		// Get SalesItems
		var saleItems = productsSaleItemsAvailable.Select(x =>
		{
			var asd = x.Where(x => x.Country == country &&
			x.Status == Common.Entities.EActivableStatus.Active &&
			x.Enabled.HasValue && x.Enabled.Value);
			var minPrices = asd.Select(x => x.Price);
			return minPrices.Max();
		});

		return saleItems.Max();
	}

}
