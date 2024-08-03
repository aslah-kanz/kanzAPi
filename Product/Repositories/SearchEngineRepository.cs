using KanzApi.Product.Models;
using Meilisearch;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace KanzApi.Product.Repositories;

public class SearchEngineRepository<T> : ISearchEngineRepository<T>
{
	private readonly Meilisearch.Index _index;
	private readonly MeilisearchClient _client;
	private readonly int _delay;
	private readonly string _serverUrl;
	private readonly string[] _filterable;
	private readonly string[] _searchable;
	private readonly string[] _sortable;
	private readonly IHttpClientFactory _httpClientFactory;

	public SearchEngineRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory)
	{
		var serverUrl = configuration.GetValue<string>("Meilisearch:BaseUrl");
		var masterKey = configuration.GetValue<string>("Meilisearch:MasterKey");
		var index = GetIndexConfig(configuration);
		
		_httpClientFactory = httpClientFactory;
		_serverUrl = serverUrl!;
		_client = new MeilisearchClient(serverUrl, masterKey);
		_index = _client.Index(index);
		_delay = configuration.GetValue<int>("Meilisearch:DelayInMs");
		_filterable = GetIndexFilterable(configuration);
		_searchable = GetIndexSearchable(configuration);
		_sortable = GetIndexSortable(configuration);
	}

	//public async Task CreateIndex()
	//{
	//	try
	//	{
	//		// Check if the index already exists
	//		var existingIndex = await _client.GetIndexAsync(_index.Uid);
	//		if (existingIndex != null)
	//		{
	//			Console.WriteLine($"Index '{_index.Uid}' already exists.");
	//			return;
	//		}
	//	}
	//	catch (MeilisearchApiError error)
	//	{
	//		Console.WriteLine(error.Message);
	//	}

	//	// Create the index
	//	await _client.CreateIndexAsync(_index.Uid);
	//	Console.WriteLine($"Index '{_index.Uid}' created successfully.");
	//}

	/// <summary>
	/// Add or Update Document
	/// </summary>
	/// <param name="document">object of the requested index in array</param>
	public async Task<TaskInfoStatus> AddOrReplace(IEnumerable<T> documents)
	{
		Console.WriteLine("Upload to meilisearch with {0} record", documents.Count());
		var task = await _index.AddDocumentsAsync(documents);
		return await WaitForTaskCompletion(task.TaskUid);
	}

	public async Task<TaskInfoStatus> Update(IEnumerable<T> documents)
	{
		var task = await _index.UpdateDocumentsAsync(documents);
		return await WaitForTaskCompletion(task.TaskUid);
	}

	public async Task<IReadOnlyCollection<T>> FindDocumentByFamilyCode(string value)
	{
		//var sq = new SearchQuery
		//{
		//	Q = $"{value}"
		//};
		var result = await _index.SearchAsync<T>(value);
		return result.Hits;
	}

	/// <summary>
	/// Delete document by id(s)
	/// </summary>
	/// <param name="ids">Id of the requested index in array</param>
	public async Task<TaskInfoStatus> Delete(IEnumerable<string> ids)
	{
		var task = await _index.DeleteDocumentsAsync(ids);
		return await WaitForTaskCompletion(task.TaskUid);
	}

	/// <summary>
	/// Set Index for Filterable
	/// </summary>
	public async Task<TaskInfoStatus> FilterableSettings()
	{
		var attributes = _filterable;
		TaskInfo task = await _index.UpdateFilterableAttributesAsync(attributes);
		return await WaitForTaskCompletion(task.TaskUid);
	}

	/// <summary>
	/// Set Index for Searchable
	/// </summary>
	public async Task<TaskInfoStatus> SearchableSettings()
	{
		var attributes = _searchable;
		TaskInfo task = await _index.UpdateSearchableAttributesAsync(attributes);
		return await WaitForTaskCompletion(task.TaskUid);
	}
	
	/// <summary>
	/// Set Index for Sortable
	/// </summary>
	public async Task<TaskInfoStatus> SortableSettings()
	{
		var attributes = _sortable;
		TaskInfo task = await _index.UpdateSortableAttributesAsync(attributes);
		return await WaitForTaskCompletion(task.TaskUid);
	}

	/// <summary>
	/// Set Product Index for Faceted
	/// </summary>
	public async Task<TaskInfoStatus> FacetedSettings()
	{
		var body = new FacetingMeilisearch()
		{
			MaxValuesPerFacet = 10,
			SortFacetValuesBy = new Dictionary<string, string>()
			{
				{ "*", "alpha" },
				{ "specificationdescen", "count" },
			}
		};

		StringContent httpContent = new(JsonSerializer.Serialize(body), Encoding.UTF8, MediaTypeNames.Application.Json);

		var httpClient = _httpClientFactory.CreateClient();
		HttpResponseMessage httpResponse = await httpClient.PostAsync(_serverUrl, httpContent);

		//var faceting = new Faceting
		//{
		//	MaxValuesPerFacet = 10
		//};
		//TaskInfo task = await _index.UpdateFacetingAsync(faceting);
		var responseContent = await httpResponse.Content.ReadAsStringAsync();
		var task = JsonSerializer.Deserialize<TaskInfo>(responseContent);

		return await WaitForTaskCompletion(task!.TaskUid);
	}

	private async Task<TaskInfoStatus> WaitForTaskCompletion(int taskUid)
	{
		while (true)
		{
			var task = await _client.GetTaskAsync(taskUid);
			if (task.Status == TaskInfoStatus.Succeeded || task.Status == TaskInfoStatus.Failed || task.Status == TaskInfoStatus.Canceled)
			{
				Console.WriteLine("Task Enqueue Succeeded");
				return task.Status;
			}
			await Task.Delay(_delay);
		}
	}

	private class FacetingMeilisearch : Faceting
	{
		public object? SortFacetValuesBy { get; set; }

	}

	private static string GetIndexConfig(IConfiguration configuration)
	{
		if (typeof(T) == typeof(ProductFamilyProductItemMeilisearch))
		{
			return configuration.GetValue<string>("Meilisearch:FamilyIndex")!;
		}
		else if(typeof(T) == typeof(ProductMeilisearch))
		{
			return configuration.GetValue<string>("Meilisearch:ProductIndex")!;
		}
		else
		{
			return "";
		}
	}

	private static string[] GetIndexFilterable(IConfiguration configuration)
	{
		if (typeof(T) == typeof(ProductFamilyProductItemMeilisearch))
		{
			return configuration.GetSection("Meilisearch:FamilyFilterable").Get<string[]>()!;
		}
		else if (typeof(T) == typeof(ProductMeilisearch))
		{
			return configuration.GetSection("Meilisearch:ProductFilterable").Get<string[]>()!;
		}
		else
		{
			return [];
		}
	}

	private static string[] GetIndexSearchable(IConfiguration configuration)
	{
		if (typeof(T) == typeof(ProductFamilyProductItemMeilisearch))
		{
			return configuration.GetSection("Meilisearch:FamilySearchable").Get<string[]>()!;
		}
		else if (typeof(T) == typeof(ProductMeilisearch))
		{
			return configuration.GetSection("Meilisearch:ProductSearchable").Get<string[]>()!;
		}
		else
		{
			return [];
		}
	}
	
	private static string[] GetIndexSortable(IConfiguration configuration)
	{
		if (typeof(T) == typeof(ProductFamilyProductItemMeilisearch))
		{
			return configuration.GetSection("Meilisearch:FamilySortable").Get<string[]>()!;
		}
		else if (typeof(T) == typeof(ProductMeilisearch))
		{
			return configuration.GetSection("Meilisearch:ProductSortable").Get<string[]>()!;
		}
		else
		{
			return [];
		}
	}

}
