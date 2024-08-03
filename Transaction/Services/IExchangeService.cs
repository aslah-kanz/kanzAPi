using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IExchangeService : ICrudService<Exchange, Guid?>
{

    ExchangeResponse AdminApprove(Guid id, string comment);

    ExchangeResponse AdminReject(Guid id, string comment);

    ExchangeResponse VendorApprove(Guid id, string comment);

    ExchangeResponse VendorReject(Guid id, string comment);

    AdminExchangeResponse GetAdminModelById(Guid id);

    PaginatedResponse<AdminExchangeResponse> FindAllAdminModels(AdminExchangePageableParam param);
}
