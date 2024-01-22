using Data.Authentication;
using Data.Entities.Identities;

namespace Services.Interface
{
    public interface IAuthenticationService
    {
        Task<string> GetJWTToken(User user);
        //public JwtSecurityToken ReadJWTToken(string accessToken);
        //public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        //public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        //public Task<string> ValidateToken(string AccessToken);
        //public Task<string> ConfirmEmail(int? userId, string? code);
        //public Task<string> SendResetPasswordCode(string Email);
        //public Task<string> ConfirmResetPassword(string Code, string Email);
        //public Task<string> ResetPassword(string Email, string Password);
    }
}
