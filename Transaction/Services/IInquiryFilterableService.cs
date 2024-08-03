using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IInquiryFilterableService : IFilterableCrudService<Inquiry, int?>
{

    InquiryResponse Add(AddInquiryRequest request);

    InquiryResponse Edit(int id, EditInquiryRequest request);

    InquiryResponse RemoveModelById(int id);

    InquiryResponse GetModelById(int id);

    PaginatedResponse<InquiryResponse> FindAllModels(InquiryPageableParam param);

    IEnumerable<int> FindAllProductIds();
}
