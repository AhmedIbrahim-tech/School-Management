using Data.Entities.Authentication;

namespace Core.Features.Authentication.Commands.Requests
{
    public class SignInCommand : IRequest<GenericBaseResponse<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
