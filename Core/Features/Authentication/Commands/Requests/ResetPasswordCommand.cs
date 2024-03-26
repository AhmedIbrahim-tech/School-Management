namespace Core.Features.Authentication.Commands.Requests;

public class ResetPasswordCommand : IRequest<GenericBaseResponse<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
