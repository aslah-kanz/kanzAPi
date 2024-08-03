using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using KanzApi.Utils;
using MapsterMapper;
using System.Transactions;

namespace KanzApi.Common.Services;

public class CertificateService(ICertificateRepository repository, IMapper mapper, IImageService imageService)
: CrudService<Certificate, int?>(repository), ICertificateService
{

    private readonly IMapper _mapper = mapper;

    private readonly IImageService _imageService = imageService;

    public CertificateResponse Add(CertificateRequest request)
    {
        Certificate entity = _mapper.Map<Certificate>(request);

        using TransactionScope scope = Transactions.Create();

        if (request.File != null)
        {
            Image image = _imageService.AddWithRandomName(request.File, EImageGroup.Certificate);
            entity.Image = image;
        }

        entity = Add(entity);
        CertificateResponse response = _mapper.Map<CertificateResponse>(entity);

        scope.Complete();

        return response;
    }

    public CertificateResponse Edit(int id, CertificateRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Certificate entity = GetById(id);
        _mapper.Map(request, entity);

        if (request.File != null)
        {
            Image image = _imageService.AddWithRandomName(request.File, EImageGroup.Certificate);
            entity.Image = image;
        }
        entity = Edit(entity);
        CertificateResponse response = _mapper.Map<CertificateResponse>(entity);

        scope.Complete();

        return response;
    }

    public CertificateResponse RemoveModelById(int id)
    {
        Certificate entity = RemoveById(id);
        return _mapper.Map<CertificateResponse>(entity);
    }

    public CertificateResponse GetModelById(int id)
    {
        Certificate entity = GetById(id);
        return _mapper.Map<CertificateResponse>(entity);
    }

    public PaginatedResponse<CertificateResponse> FindAllModels(CertificatePageableParam param)
    {
        PaginatedEntity<Certificate> pEntity = FindAll(param);
        IEnumerable<CertificateResponse> models = pEntity.Content.Select(_mapper.Map<CertificateResponse>);

        return PaginatedResponse<CertificateResponse>.From(pEntity, models);
    }
}
