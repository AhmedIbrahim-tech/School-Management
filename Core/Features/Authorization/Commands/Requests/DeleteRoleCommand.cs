namespace Core.Features.Authorization.Commands.Requests;

public class DeleteRoleCommand : IRequest<GenericBaseResponse<string>>
{
    public int Id { get; set; }
    public DeleteRoleCommand(int id)
    {
        Id = id;
    }
}
