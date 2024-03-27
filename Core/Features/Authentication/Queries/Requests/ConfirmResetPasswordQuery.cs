namespace Core.Features.Authentication.Queries.Requests;

public class ConfirmResetPasswordQuery : IRequest<GenericBaseResponse<string>>
{
    public string Code { get; set; }
    public string Email { get; set; }
}
