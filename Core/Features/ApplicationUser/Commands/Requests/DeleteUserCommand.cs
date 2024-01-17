namespace Core.Features.ApplicationUser.Commands.Requests;

public class DeleteUserCommand : IRequest<GenericBaseResponse<string>>
{
    public int Id { get; set; }
    public DeleteUserCommand(int id)
    {
        Id = id;
    }
}
