using Core.Features.Department.Queries.Results;

namespace Core.Features.Department.Queries.Models;

public class GetSingleDepartmentQuery : IRequest<GenericBaseResponse<GetSingleDepartmentResponse>>
{
    public int Id { get; set; }

    public GetSingleDepartmentQuery(int id)
    {
        Id = id;
    }

    public GetSingleDepartmentQuery()
    {

    }
}
