using KanzApi.Product.Entities;
using Meilisearch;

namespace KanzApi.Product.Services;

public interface ISearchEngineService<T>
{
    Task<TaskInfoStatus> SyncProducts();
	Task<TaskInfoStatus> AddOrReplace(IEnumerable<T> documents);
	Task<TaskInfoStatus> Update(IEnumerable<T> documents);
	Task<IReadOnlyCollection<T>> FindDocumentByFamilyCode(string fieldName, string value);
	Task<TaskInfoStatus> UpdateDocumentProductStatus(string value, int id, EProductStatus status);
	Task<TaskInfoStatus> SyncProductsById(List<int> ids);
	TaskInfoStatus SyncProductsByFamilyCode(string code, int? deletedId = null);
	Task<TaskInfoStatus> Delete(IEnumerable<string> ids);
	Task<TaskInfoStatus> FilterableSettings(string indexName);
	Task<TaskInfoStatus> SearchableSettings(string indexName);
	Task<TaskInfoStatus> SortableSettings(string indexName);
	Task<TaskInfoStatus> FacetedSettings(string indexName);
}
