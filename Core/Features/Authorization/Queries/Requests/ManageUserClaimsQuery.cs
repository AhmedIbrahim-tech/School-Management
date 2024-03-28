namespace Core.Features.Authorization.Queries.Requests;

public class ManageUserClaimsQuery : IRequest<GenericBaseResponse<ManageUserClaimsResult>>
{
    public int UserId { get; set; }
}
