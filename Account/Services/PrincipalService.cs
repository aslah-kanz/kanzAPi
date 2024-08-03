using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Messaging.Services;
using KanzApi.Security.Models;
using KanzApi.Security.Services;
using KanzApi.Utils;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Account.Services;

public class PrincipalService(IPrincipalRepository repository,
IMapper mapper, ISessionService sessionService, IPasswordService passwordService,
INotificationFilterableService notificationService, IMailService mailService)
: CrudService<Principal, int?>(repository), IPrincipalService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPasswordService _passwordService = passwordService;

    private readonly INotificationFilterableService _notificationService = notificationService;

    private readonly IMailService _mailService = mailService;

    public PrincipalResponse Add(PrincipalRequest request)
    {
        Principal entity = _mapper.Map<Principal>(request);

        Principal? principal = GetByUsername(request.Username!);
        if (principal?.Username == request.Username)
        {
            throw new UsernameAlreadyExistException(request.Username!);
        }

        entity.Status = EPrincipalStatus.Active;

        string password = _passwordService.Generate();
        entity.Password = _passwordService.Hash(password);

        using TransactionScope scope = Transactions.Create();

        entity = Add(entity);

        _mailService.Send(entity, password);

        PrincipalResponse response = _mapper.Map<PrincipalResponse>(entity);

        scope.Complete();

        return response;
    }

    public Principal Add(SignupRequest request, bool generatePassword)
    {
        Principal entity = _mapper.Map<Principal>(request);
        if (generatePassword)
        {
            entity.Password = _passwordService.Hash(request.Password!);
        }
        else
        {
            _notificationService.Send(entity, FindAllByType(EPrincipalType.Admin));
        }

        return Add(entity);
    }

    public Principal Add(IMemberRequest request)
    {
        Principal entity = _mapper.Map<Principal>(request);
        entity.Username = entity.Email;
        entity.Status = EPrincipalStatus.Active;

        string password = _passwordService.Generate();
        entity.Password = _passwordService.Hash(password);

        using TransactionScope scope = Transactions.Create();

        entity = Add(entity);

        _mailService.Send(entity, password);

        scope.Complete();

        return entity;
    }

    public PrincipalResponse Edit(int id, EditPrincipalRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Principal entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        PrincipalResponse response = _mapper.Map<PrincipalResponse>(entity);

        scope.Complete();

        return response;
    }

    public Principal Edit(int principalId, CompanyMemberRequest request)
    {
        Principal entity = GetById(principalId);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return entity;
    }

    public Principal RequestApproval()
    {
        Principal principal = GetCurrent();
        principal.Status = EPrincipalStatus.PendingApproval;

        return Edit(principal);
    }

    public PrincipalResponse Approve(int id)
    {
        Principal entity = GetById(id);
        entity.Status = EPrincipalStatus.Active;

        if (entity.Password == null)
        {
            string password = _passwordService.Generate();
            entity.Password = _passwordService.Hash(password);

            _mailService.Send(entity, password);
        }
        else
        {
            _mailService.Send(entity, true);
        }

        entity = Edit(entity);

        return _mapper.Map<PrincipalResponse>(entity);
    }

    public PrincipalResponse Reject(int id)
    {
        Principal entity = GetById(id);
        entity.Status = EPrincipalStatus.PendingPrincipalDetail;
        entity = Edit(entity);

        _mailService.Send(entity, false);

        return _mapper.Map<PrincipalResponse>(entity);
    }

    public PrincipalResponse RemoveModelById(int id)
    {
        Principal entity = RemoveById(id);
        return _mapper.Map<PrincipalResponse>(entity);
    }

    public Principal Disable(int principalId)
    {
        Principal entity = GetById(principalId);
        entity.Status = EPrincipalStatus.Disabled;

        entity = Edit(entity);

        return entity;
    }

    public Principal GetByEmail(string email)
    {
        return FindByPredicate(Principal.QEmailEquals(email))
        ?? throw EntityNotFoundException.From(typeof(Principal), "Email", email);
    }

    public Principal? GetByUsername(string username)
    {
        return FindByPredicate(Principal.QUsernameEquals(username));
    }

    public Principal? FindCurrent()
    {
        return FindById(_sessionService.CurrentPrincipalId());
    }

    public Principal GetCurrent()
    {
        return FindCurrent()
        ?? throw EntityNotFoundException.From(typeof(Principal), "Principal ID", _sessionService.CurrentPrincipalId());
    }

    public Principal? FindByUsername(string username)
    {
        return FindByPredicate(Principal.QUsernameEquals(username));
    }

    public Principal GetByUsernameAndPassword(string username, string password, params EPrincipalType[]? types)
    {
        Expression<Func<Principal, bool>> predicate = Principal.QUsernameEquals(username);
        if (types?.Length > 0)
        {
            Expression<Func<Principal, bool>>? typesPredicate = null;
            foreach (EPrincipalType type in types)
            {
                typesPredicate = typesPredicate != null
                ? typesPredicate.Or(Principal.QTypeEquals(type)) : Principal.QTypeEquals(type);
            }
            predicate = predicate.And(typesPredicate!);
        }

        Principal? entity = FindByPredicate(predicate);

        if (entity == null || !_passwordService.Verify(password!, entity.Password!))
        {
            throw new UsernameAndPasswordNotFoundException();
        }

        return entity;
    }

    public PrincipalResponse GetModelById(int id)
    {
        Principal entity = GetById(id);
        return _mapper.Map<PrincipalResponse>(entity);
    }

    public IEnumerable<Principal> FindAllByType(EPrincipalType type)
    {
        return FindAll(Principal.QTypeEquals(type), null);
    }

    public PaginatedResponse<PrincipalResponse> FindAllModels(PrincipalPageableParam param)
    {
        PaginatedEntity<Principal> pEntity = FindAll(param);
        IEnumerable<PrincipalResponse> models = pEntity.Content.Select(_mapper.Map<PrincipalResponse>);

        return PaginatedResponse<PrincipalResponse>.From(pEntity, models);
    }
}
