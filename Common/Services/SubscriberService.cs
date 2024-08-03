using MapsterMapper;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Repositories;

namespace KanzApi.Common.Services;

public class SubscriberService(ISubscriberRepository repository, IMapper mapper)
: CrudService<Subscriber, int?>(repository), ISubscriberService
{

    private readonly IMapper _mapper = mapper;

    public SubscriberResponse Add(SubscriberRequest request)
    {
        Subscriber entity = _mapper.Map<Subscriber>(request);
        entity = Add(entity);

        return _mapper.Map<SubscriberResponse>(entity);
    }
}
