using KanzApi.Common.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Common.Services;

public interface IDocumentService : ICrudService<Document, long?>
{

    string FilePath(Document entity);

    Document Add(IFormFile file, string? name);

    DocumentResponse Add(DocumentRequest request);

    Document AddWithRandomName(IFormFile file);

    DocumentResponse Edit(long id, DocumentRequest request);

    DocumentResponse RemoveModelById(long id);

    Document GetByName(string name);

    DocumentResponse GetModelById(long id);
}
