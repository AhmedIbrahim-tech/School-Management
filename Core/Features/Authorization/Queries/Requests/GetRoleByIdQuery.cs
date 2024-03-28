namespace Core.Features.Authorization.Queries.Requests;

public class GetRoleByIdQuery : IRequest<GenericBaseResponse<GetRoleByIdResult>>
{
    public int Id { get; set; }
}
