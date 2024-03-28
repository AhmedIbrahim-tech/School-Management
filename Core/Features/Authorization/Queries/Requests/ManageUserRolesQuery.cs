namespace Core.Features.Authorization.Queries.Requests;

public class ManageUserRolesQuery : IRequest<GenericBaseResponse<ManageUserRolesResult>>
{
    public int UserId { get; set; }
}
