using System.Security.Cryptography;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Repositories;
using MapsterMapper;

namespace KanzApi.Common.Services;

public class DocumentService(IDocumentRepository repository, IMapper mapper, IConfiguration configuration)
: CrudService<Document, long?>(repository), IDocumentService
{

    private readonly IMapper _mapper = mapper;

    private readonly IConfiguration _configuration = configuration;

    private string StorageDir()
    {
        string path = Path.Combine(_configuration.GetValue<string>("KanzApi:StorageDir")!, "documents");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }

    public string FilePath(Document entity)
    {
        return Path.Combine(StorageDir(), entity.Name!);
    }

    private void SaveFile(IFormFile file, string? name, Document entity)
    {
        string dir = StorageDir();

        RemoveFile(dir, entity);

        if (String.IsNullOrWhiteSpace(name)) name = file.FileName;

        string path = Path.Combine(dir, name);

        if (!name.Equals(entity.Name) && File.Exists(path))
            throw new FileAlreadyExistException(file.FileName);

        using (FileStream stream = new(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        entity.Name = name;
        entity.Url = _configuration.GetValue<string>("KanzApi:BaseUrl") + "/documents/download/" + name;
        entity.Type = file.ContentType;
    }

    private void RemoveFile(string dir, Document entity)
    {
        if (entity.Name != null)
        {
            string prevPath = Path.Combine(dir, entity.Name!);
            if (File.Exists(prevPath))
            {
                File.Delete(prevPath);
            }
        }
    }

    public Document Add(IFormFile file, string? name)
    {
        Document entity = new();
        SaveFile(file, name, entity);

        return Add(entity);
    }

    public Document AddWithRandomName(IFormFile file)
    {
        RandomNumberGenerator generator = RandomNumberGenerator.Create();

        byte[] randomName = new byte[16];
        generator.GetBytes(randomName);

        string extension = Path.GetExtension(file.FileName);
        string name = Convert.ToHexString(randomName) + extension;

        return Add(file, name);
    }

    public DocumentResponse Add(DocumentRequest request)
    {
        Document entity = AddWithRandomName(request.File!);

        return _mapper.Map<DocumentResponse>(entity);
    }

    public DocumentResponse Edit(long id, DocumentRequest request)
    {
        Document entity = GetById(id);
        SaveFile(request.File!, request.Name, entity);

        entity = Edit(entity);

        return _mapper.Map<DocumentResponse>(entity);
    }

    public override Document Remove(Document entity)
    {
        RemoveFile(StorageDir(), entity);

        return base.Remove(entity);
    }

    public DocumentResponse RemoveModelById(long id)
    {
        Document entity = RemoveById(id);
        return _mapper.Map<DocumentResponse>(entity);
    }

    public Document GetByName(string name)
    {
        return FindByPredicate(Document.QNameEquals(name))
        ?? throw EntityNotFoundException.From(typeof(Document), "Name", name);
    }

    public DocumentResponse GetModelById(long id)
    {
        Document entity = GetById(id);
        return _mapper.Map<DocumentResponse>(entity);
    }
}
