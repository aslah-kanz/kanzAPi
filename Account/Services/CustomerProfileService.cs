using System.Transactions;
using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Repositories;
using KanzApi.Common.Entities;
using KanzApi.Common.Services;
using KanzApi.Utils;
using MapsterMapper;

namespace KanzApi.Account.Services;

public class CustomerProfileService(IPrincipalRepository repository,
ISessionService sessionService, IMapper mapper, IImageService imageService)
: CrudService<Principal, int?>(repository), ICustomerProfileService
{

    private readonly IMapper _mapper = mapper;

    private readonly ISessionService _sessionService = sessionService;

    private readonly IImageService _imageService = imageService;

    public CustomerProfileResponse Edit(CustomerProfileRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        int principalId = (int)_sessionService.CurrentPrincipalId()!;

        Principal entity = GetById(principalId);
        _mapper.Map(request, entity);

        if (request.File != null)
        {
            Image image = _imageService.AddWithRandomName(request.File, EImageGroup.CustomerProfile);
            entity.Image = image;
        }

        entity = Edit(entity);

        CustomerProfileResponse response = _mapper.Map<CustomerProfileResponse>(entity);

        scope.Complete();

        return response;
    }

    public CustomerProfileResponse GetModelById()
    {
        int principalId = (int)_sessionService.CurrentPrincipalId()!;

        Principal entity = GetById(principalId);
        return _mapper.Map<CustomerProfileResponse>(entity);
    }
}
