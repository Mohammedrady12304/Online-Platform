using E_Learning.Areas.Authentication.Models;
namespace E_Learning.Services.IService
{
    public interface IAuthService
    {
        Task<ProcessResult> RegisterAsync(RegisterRequest model);
        Task<ProcessResult> SendConfirmationEmailAsync(ConfrimEmailRequest model);
        Task ConfirmEmailAsync(string Email);
        Task<ProcessResult> SendResetPassowrdEmail(ConfrimEmailRequest model);
        Task<ProcessResult> ResetPasswordAsync(ResetPasswordRequest model);
        Task<ProcessResult> ChangePasswordAsync(ChangePasswordRequest model);
        Task<ProcessResult> LoginAsync(LoginRequest model);
        Task LogoutAsync();
    }
}
