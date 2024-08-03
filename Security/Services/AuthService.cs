using System.Security.Claims;
using System.Transactions;
using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Services;
using KanzApi.Common.Entities;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Messaging.Services;
using KanzApi.Security.Entities;
using KanzApi.Security.Models;
using KanzApi.Utils;
using MapsterMapper;

namespace KanzApi.Security.Services;

public class AuthService(IMapper mapper,
IPrincipalService principalService, IPrincipalDetailFilterableService principalDetailService,
IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService,
IOneTimeTokenService oneTimeTokenService, IOtpService otpService,
ISessionService sessionService, IPasswordService passwordService, IMailService mailService)
: IAuthService
{

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

    private readonly IOneTimeTokenService _oneTimeTokenService = oneTimeTokenService;

    private readonly IOtpService _otpService = otpService;

    private readonly ISessionService _sessionService = sessionService;

    private readonly IPasswordService _passwordService = passwordService;

    private readonly IMailService _mailService = mailService;

    private AuthenticateResponse Authenticate(Principal principal)
    {
        if (principal.Status <= EPrincipalStatus.Inactive)
        {
            throw new UserNotActiveException();
        }

        using TransactionScope scope = Transactions.Create();

        TokenResponse accessToken = _jwtTokenService.CreateForOtp(principal);
        RefreshToken refreshToken = _refreshTokenService.Create(principal, accessToken.Id!);

        Otp otp = _otpService.Create(principal);
        AuthenticateResponse response = new()
        {
            Principal = _mapper.Map<AuthenticatePrincipalResponse>(principal),
            AccessToken = accessToken,
            RefreshToken = new TokenResponse()
            {
                Token = refreshToken.Token!,
                ExpiredAt = (DateTime)refreshToken.ExpiredAt!
            },
            OtpCode = otp.Code!
        };

        scope.Complete();

        return response;
    }

    public AuthenticateResponse Login(AuthenticateRequest request)
    {
        Principal principal = _principalService.GetByUsernameAndPassword(
            request.Username!, request.Password!, request.Types?.ToArray());
        return Authenticate(principal);
    }

    public void Signup(SignupRequest request)
    {
        if (_principalService.FindByUsername(request.Username!) != null)
        {
            throw new UsernameAlreadyExistException(request.Username!);
        }

        using TransactionScope scope = Transactions.Create();

        Principal principal = _principalService.Add(request, true);

        OneTimeToken token = _oneTimeTokenService.Create(principal, EOneTimeTokenType.Activation);

        _mailService.Send(principal, token);

        scope.Complete();
    }

    public void Signup(VendorSignupRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Principal principal = _principalService.Add(request, false);
        _principalDetailService.Add(principal, request.Detail!);

        scope.Complete();
    }

    public AuthenticateResponse Activate(ActivateRequest request)
    {
        OneTimeToken token = _oneTimeTokenService.Validate(request.Token!, EOneTimeTokenType.Activation);

        using TransactionScope scope = Transactions.Create();

        Principal principal = token.Principal!;
        principal.Status = principal.Type == EPrincipalType.Individual
        ? EPrincipalStatus.Active : EPrincipalStatus.PendingPrincipalDetail;

        _principalService.Edit(principal);

        _oneTimeTokenService.Remove(token);

        AuthenticateResponse response = Authenticate(principal);

        scope.Complete();

        return response;
    }

    public TokenResponse RefreshToken(RefreshTokenRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        ClaimsPrincipal claimsPrincipal = _jwtTokenService.ExtractPrincipal(request.AccessToken!);

        int principalId = (int)claimsPrincipal.PrincipalId()!;
        Principal principal = _principalService.GetById(principalId);

        string accessTokenId = claimsPrincipal.TokenId()!;

        RefreshToken refreshToken = _refreshTokenService.GetByPrincipalId(principalId);
        _refreshTokenService.Validate(refreshToken, request.RefreshToken!, accessTokenId);

        TokenResponse accessToken = _jwtTokenService.Create(principal);

        scope.Complete();

        return accessToken;
    }

    public void ResendOtp()
    {
        using TransactionScope scope = Transactions.Create();

        Principal principal = _principalService.GetCurrent();
        _otpService.Recreate(principal);

        scope.Complete();
    }


    public bool ValidateOTPCode(OtpRequest request)
    {
        Otp otp = _otpService.Validate(request);
        return otp != null;
    }
    public TokenResponse ValidateOtp(OtpRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Otp otp = _otpService.Validate(request);
        Principal principal = otp.Principal!;

        string tokenId = _sessionService.CurrentTokenId()!;

        RefreshToken refreshToken = _refreshTokenService.GetByPrincipalId((int)principal.Id!);
        _refreshTokenService.Validate(refreshToken, refreshToken.Token!, tokenId);

        TokenResponse accessToken = _jwtTokenService.Create(principal);

        refreshToken.AccessTokenId = accessToken.Id;
        _refreshTokenService.Edit(refreshToken);

        _otpService.Remove(otp);

        scope.Complete();

        return accessToken;
    }

    public void Logout()
    {
        int principalId = (int)_sessionService.CurrentPrincipalId()!;
        RefreshToken? refreshToken = _refreshTokenService.FindByPrincipalId(principalId);
        if (refreshToken != null)
        {
            _refreshTokenService.Remove(refreshToken);
        }
    }

    public void ForgotPassword(ForgotPasswordRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Principal principal = _principalService.GetByEmail(request.Email!);
        OneTimeToken token = _oneTimeTokenService.Create(principal, EOneTimeTokenType.ResetPassword);

        _mailService.Send(principal, token);

        scope.Complete();
    }

    public void ResetPassword(ResetPasswordRequest request)
    {
        OneTimeToken token = _oneTimeTokenService
        .Validate(request.Token!, EOneTimeTokenType.ResetPassword);

        Principal principal = token.Principal!;
        principal.Password = _passwordService.Hash(request.Password!);

        using TransactionScope scope = Transactions.Create();

        _principalService.Edit(principal);

        _oneTimeTokenService.Remove(token);

        scope.Complete();
    }

    public void ChangePassword(ChangePasswordRequest request)
    {
        Principal principal = _principalService.GetCurrent();

        if (_passwordService.Verify(request.CurrentPassword!, principal!.Password!))
        {
            principal!.Password = _passwordService.Hash(request.NewPassword!);

            _principalService.Edit(principal);
        }
        else throw new InvalidPasswordException();
    }
}
