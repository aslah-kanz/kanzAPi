using System.Security.Cryptography;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using KanzApi.Extensions;
using KanzApi.Utils;
using KanzApi.Vendors.Azure.Services;
using MapsterMapper;
using SkiaSharp;

namespace KanzApi.Common.Services;

public class ImageService(IImageRepository repository,
IMapper mapper, IAzureStorageService azureStorageService)
: CrudService<Image, long?>(repository), IImageService
{

    private readonly IMapper _mapper = mapper;

    private readonly IAzureStorageService _azureStorageService = azureStorageService;

    private void SaveFile(IFormFile file, EImageGroup? group, string? name, Image entity)
    {
        if (String.IsNullOrWhiteSpace(name)) name = file.FileName;

        entity.Name = name;
        entity.Group = group;
        entity.Type = file.ContentType;

        using Stream stream = file.OpenReadStream();

        _azureStorageService.Upload(stream, Constants.ImageStorageContainer, entity.ToPath());

        stream.Seek(0, SeekOrigin.Begin);

        SKImageInfo info = SKBitmap.DecodeBounds(stream);
        entity.Width = info.Width;
        entity.Height = info.Height;
    }

    private void RemoveFile(Image entity)
    {
        if (entity.Name != null)
        {
            _azureStorageService.Delete(Constants.ImageStorageContainer, entity.ToPath());
        }
    }

    public Image Add(IFormFile file, EImageGroup? group, string? name)
    {
        Image entity = new();
        SaveFile(file, group, name, entity);

        return Add(entity);
    }

    public Image AddWithRandomName(IFormFile file, EImageGroup? group)
    {
        RandomNumberGenerator generator = RandomNumberGenerator.Create();

        byte[] randomName = new byte[16];
        generator.GetBytes(randomName);

        string extension = Path.GetExtension(file.FileName);
        string name = Convert.ToHexString(randomName) + extension;

        return Add(file, group, name);
    }

    public ImageResponse Add(ImageRequest request)
    {
        Image entity = Add(request.File!, request.Group, request.Name);
        return _mapper.Map<ImageResponse>(entity);
    }

    public ImageResponse Edit(long id, ImageRequest request)
    {
        Image entity = GetById(id);
        SaveFile(request.File!, (EImageGroup)request.Group!, request.Name, entity);

        entity = Edit(entity);

        return _mapper.Map<ImageResponse>(entity);
    }

    public override Image Remove(Image entity)
    {
        RemoveFile(entity);

        return base.Remove(entity);
    }

    public ImageResponse RemoveModelById(long id)
    {
        Image entity = RemoveById(id);
        return _mapper.Map<ImageResponse>(entity);
    }

    public Image GetByGroupAndName(EImageGroup group, string name)
    {
        return FindByPredicate(Image.QGroupEquals(group).And(Image.QNameEquals(name)))
        ?? throw EntityNotFoundException.From(typeof(Image), new Dictionary<string, string>
        {{"Group", group.ToString()}, {"Name", name}});
    }

    public Image GetByName(string name)
    {
        return FindByPredicate(Image.QGroupIsNull().And(Image.QNameEquals(name)))
        ?? throw EntityNotFoundException.From(typeof(Image), "Name", name);
    }

    public ImageResponse GetModelById(long id)
    {
        Image entity = GetById(id);
        return _mapper.Map<ImageResponse>(entity);
    }

    public Stream Download(Image entity)
    {
        return _azureStorageService.Download(Constants.ImageStorageContainer, entity.ToPath());
    }

    public PaginatedResponse<ImageResponse> FindAllModels(ImagePageableParam param)
    {
        PaginatedEntity<Image> pEntity = FindAll(param);
        IEnumerable<ImageResponse> models = pEntity.Content.Select(_mapper.Map<ImageResponse>);

        return PaginatedResponse<ImageResponse>.From(pEntity, models);
    }
}
