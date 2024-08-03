using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using MapsterMapper;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public class CatalogueService(ICatalogueRepository repository, IMapper mapper)
: CrudService<Catalogue, int?>(repository), ICatalogueService
{

    private readonly IMapper _mapper = mapper;

    public CatalogueResponse Add(CatalogueRequest request)
    {
        Catalogue entity = _mapper.Map<Catalogue>(request);
        entity = Add(entity);

        return _mapper.Map<CatalogueResponse>(entity);
    }

    public CatalogueResponse Edit(int id, CatalogueRequest request)
    {
        Catalogue entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<CatalogueResponse>(entity);
    }

    public CatalogueResponse RemoveModelById(int id)
    {
        Catalogue entity = RemoveById(id);
        return _mapper.Map<CatalogueResponse>(entity);
    }

    public CatalogueResponse GetModelById(int id)
    {
        Catalogue entity = GetById(id);
        return _mapper.Map<CatalogueResponse>(entity);
    }

    public PaginatedResponse<CatalogueResponse> FindAllModels(CataloguePageableParam param)
    {
        PaginatedEntity<Catalogue> pEntity = FindAll(param);
        IEnumerable<CatalogueResponse> models = pEntity.Content.Select(_mapper.Map<CatalogueResponse>);

        return PaginatedResponse<CatalogueResponse>.From(pEntity, models);
    }
}
