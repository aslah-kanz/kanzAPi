using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using MapsterMapper;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;
using System.Transactions;
using KanzApi.Utils;

namespace KanzApi.Common.Services;

public class BannerService(IBannerRepository repository, IMapper mapper, IImageService imageService)
: CrudService<Banner, int?>(repository), IBannerService
{

    private readonly IMapper _mapper = mapper;

    private readonly IImageService _imageService = imageService;

    public BannerResponse Add(BannerRequest request)
    {
        Banner entity = _mapper.Map<Banner>(request);

        using TransactionScope scope = Transactions.Create();

        if (request.File != null)
        {
            Image image = _imageService.AddWithRandomName(request.File, EImageGroup.Banner);
            entity.Image = image;
        }

        entity = Add(entity);
        BannerResponse response = _mapper.Map<BannerResponse>(entity);

        scope.Complete();

        return response;
    }

    public BannerResponse Edit(int id, BannerRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Banner entity = GetById(id);
        _mapper.Map(request, entity);

        if (request.File != null)
        {
            Image image = _imageService.AddWithRandomName(request.File, EImageGroup.Banner);
            entity.Image = image;
        }

        entity = Edit(entity);
        BannerResponse response = _mapper.Map<BannerResponse>(entity);

        scope.Complete();

        return response;
    }

    public BannerResponse RemoveModelById(int id)
    {
        Banner entity = RemoveById(id);
        return _mapper.Map<BannerResponse>(entity);
    }

    public BannerResponse GetModelById(int id)
    {
        Banner entity = GetById(id);
        return _mapper.Map<BannerResponse>(entity);
    }

    public PaginatedResponse<BannerResponse> FindAllModels(BannerPageableParam param)
    {
        PaginatedEntity<Banner> pEntity = FindAll(param);
        IEnumerable<BannerResponse> models = pEntity.Content.Select(_mapper.Map<BannerResponse>);

        return PaginatedResponse<BannerResponse>.From(pEntity, models);
    }
}
