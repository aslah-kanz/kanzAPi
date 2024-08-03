using KanzApi.Common.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Common.Services;

public interface ISubscriberService : ICrudService<Subscriber, int?>
{

    SubscriberResponse Add(SubscriberRequest request);
}
