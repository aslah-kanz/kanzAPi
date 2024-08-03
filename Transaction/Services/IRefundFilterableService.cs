using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IRefundFilterableService : IFilterableCrudService<Refund, Guid?>
{

    RefundResponse Add(RefundRequest request);

    RefundResponse AdminApprove(Guid id, string comment);

    RefundResponse AdminReject(Guid id, string comment);

    RefundResponse VendorApprove(Guid id, string comment);

    RefundResponse VendorReject(Guid id, string comment);

    RefundResponse RemoveModelById(Guid id);

    RefundResponse GetModelById(Guid id);

    AdminRefundResponse GetAdminModelById(Guid id);

    PaginatedResponse<RefundResponse> FindAllModels(RefundPageableParam param);

    PaginatedResponse<AdminRefundResponse> FindAllAdminModels(AdminRefundPageableParam param);
}
