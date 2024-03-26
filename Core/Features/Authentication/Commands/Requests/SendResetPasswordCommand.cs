namespace Core.Features.Authentication.Commands.Requests;

public class SendResetPasswordCommand : IRequest<GenericBaseResponse<string>>
{
    public string Email { get; set; }
}
