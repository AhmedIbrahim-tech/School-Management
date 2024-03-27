namespace Core.Features.Authentication.Queries.Requests;

public class ConfirmEmailQuery : IRequest<GenericBaseResponse<string>>
{
    public int UserId { get; set; }
    public string Code { get; set; }
}
