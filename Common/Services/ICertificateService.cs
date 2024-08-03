using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface ICertificateService : ICrudService<Certificate, int?>
{
    CertificateResponse Add(CertificateRequest request);

    CertificateResponse Edit(int id, CertificateRequest request);

    CertificateResponse RemoveModelById(int id);

    CertificateResponse GetModelById(int id);

    PaginatedResponse<CertificateResponse> FindAllModels(CertificatePageableParam param);
}
