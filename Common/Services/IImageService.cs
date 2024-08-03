using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IImageService : ICrudService<Image, long?>
{

    Image Add(IFormFile file, EImageGroup? group, string? name);

    Image AddWithRandomName(IFormFile file, EImageGroup? group);

    ImageResponse Add(ImageRequest request);

    ImageResponse Edit(long id, ImageRequest request);

    ImageResponse RemoveModelById(long id);

    Image GetByGroupAndName(EImageGroup group, string name);

    Image GetByName(string name);

    ImageResponse GetModelById(long id);

    Stream Download(Image entity);

    PaginatedResponse<ImageResponse> FindAllModels(ImagePageableParam param);
}
