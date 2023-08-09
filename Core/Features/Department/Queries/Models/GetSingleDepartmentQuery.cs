namespace Core.Features.Department.Queries.Models;

public class GetSingleDepartmentQuery : IRequest<GenericBaseResponse<GetSingleDepartmentResponse>>
{
    public int Id { get; set; }
    public int StudentPageNumber { get; set; }
    public int StudentPageSize { get; set; }
}
