using Data.Entities.Authentication;

namespace Core.Features.Authentication.Commands.Requests;

public class RefreshTokenCommand : IRequest<GenericBaseResponse<JwtAuthResult>>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
