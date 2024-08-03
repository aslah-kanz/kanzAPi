namespace KanzApi.Vendors.Azure.Services;

public interface IAzureStorageService
{

    void Upload(Stream source, string container, string path);

    Stream Download(string container, string path);

    void Delete(string container, string path);
}
