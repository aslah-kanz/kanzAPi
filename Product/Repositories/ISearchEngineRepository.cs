using Meilisearch;

namespace KanzApi.Product.Repositories;

public interface ISearchEngineRepository<T>
{
	//Task CreateIndex();

	Task<TaskInfoStatus> AddOrReplace(IEnumerable<T> documents);

	Task<TaskInfoStatus> Update(IEnumerable<T> documents);

	Task<IReadOnlyCollection<T>> FindDocumentByFamilyCode(string value);
	
	Task<TaskInfoStatus> Delete(IEnumerable<string> ids);

	Task<TaskInfoStatus> FilterableSettings();

	Task<TaskInfoStatus> SearchableSettings();

	Task<TaskInfoStatus> SortableSettings();

	Task<TaskInfoStatus> FacetedSettings();
}
