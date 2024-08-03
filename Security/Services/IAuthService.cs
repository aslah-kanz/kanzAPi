using KanzApi.Security.Models;

namespace KanzApi.Security.Services;

public interface IAuthService
{

    AuthenticateResponse Login(AuthenticateRequest request);

    void Signup(SignupRequest request);

    void Signup(VendorSignupRequest request);

    AuthenticateResponse Activate(ActivateRequest request);

    TokenResponse RefreshToken(RefreshTokenRequest request);

    void ResendOtp();

    TokenResponse ValidateOtp(OtpRequest request);
    bool ValidateOTPCode(OtpRequest request);

    void Logout();

    void ForgotPassword(ForgotPasswordRequest request);

    void ResetPassword(ResetPasswordRequest request);

    void ChangePassword(ChangePasswordRequest request);
}
