using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace KanzApi.Vendors.Azure.Services;

public class AzureStorageService : IAzureStorageService
{

    private readonly IConfiguration _configuration;

    private readonly BlobServiceClient _serviceClient;

    private BlobContainerClient CreateContainer(string container)
    {
        BlobContainerClient containerClient = _serviceClient.GetBlobContainerClient(container);
        containerClient.CreateIfNotExists(PublicAccessType.Blob);

        return containerClient;
    }

    public AzureStorageService(IConfiguration configuration)
    {
        _configuration = configuration;

        string connectionString = _configuration.GetValue<string>("AzureStorage:ConnectionString")!;
        _serviceClient = new(connectionString);
    }

    public void Upload(Stream source, string container, string path)
    {
        BlobContainerClient containerClient = CreateContainer(container);
        BlobClient client = containerClient.GetBlobClient(path);
        client.Upload(source, true);
    }

    public Stream Download(string container, string path)
    {
        BlobContainerClient containerClient = CreateContainer(container);
        BlobClient client = containerClient.GetBlobClient(path);
        return client.OpenRead();
    }

    public void Delete(string container, string path)
    {
        BlobContainerClient containerClient = CreateContainer(container);
        BlobClient client = containerClient.GetBlobClient(path);
        client.Delete();
    }
}
