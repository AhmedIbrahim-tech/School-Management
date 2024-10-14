using Data.Entities.Authentication;
using Data.Entities.Identities;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Interface
{
    public interface IAuthenticationService
    {
        Task<JwtAuthResult> GetJWTToken(User user);
        JwtSecurityToken ReadJWTToken(string accessToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        Task<string> ValidateToken(string AccessToken);
        Task<string> ConfirmEmail(int? userId, string? code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ConfirmResetPassword(string code, string Email);
        public Task<string> ResetPassword(string Email, string Password);
    }
}
