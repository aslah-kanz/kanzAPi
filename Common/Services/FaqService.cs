using MapsterMapper;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public class FaqService(IFaqRepository repository, IMapper mapper, IFaqGroupService faqGroupService)
: CrudService<Faq, int?>(repository), IFaqService
{

    private readonly IMapper _mapper = mapper;

    private readonly IFaqGroupService _faqGroupService = faqGroupService;

    public FaqResponse Add(FaqRequest request)
    {
        Faq entity = _mapper.Map<Faq>(request);
        entity = Add(entity);

        return _mapper.Map<FaqResponse>(entity);
    }

    public FaqResponse Edit(int id, FaqRequest request)
    {
        Faq entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<FaqResponse>(entity);
    }

    public FaqResponse RemoveModelById(int id)
    {
        Faq entity = RemoveById(id);
        return _mapper.Map<FaqResponse>(entity);
    }

    public FaqResponse GetModelById(int id)
    {
        Faq entity = GetById(id);
        return _mapper.Map<FaqResponse>(entity);
    }

    public PaginatedResponse<FaqResponse> FindAllModels(FaqPageableParam param)
    {
        PaginatedEntity<Faq> pEntity = FindAll(param);
        IEnumerable<FaqResponse> models = pEntity.Content.Select(_mapper.Map<FaqResponse>);

        return PaginatedResponse<FaqResponse>.From(pEntity, models);
    }
}
