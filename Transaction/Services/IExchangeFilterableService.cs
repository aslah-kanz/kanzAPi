using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IExchangeFilterableService : IFilterableCrudService<Exchange, Guid?>
{

    ExchangeResponse Add(ExchangeRequest request);

    ExchangeResponse RemoveModelById(Guid id);

    ExchangeResponse GetModelById(Guid id);

    PaginatedResponse<ExchangeResponse> FindAllModels(ExchangePageableParam param);
}
