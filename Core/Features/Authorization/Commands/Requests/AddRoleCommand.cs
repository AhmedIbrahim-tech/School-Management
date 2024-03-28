namespace Core.Features.Authorization.Commands.Requests;

public class AddRoleCommand : IRequest<GenericBaseResponse<string>>
{
    public string RoleName { get; set; }
}
