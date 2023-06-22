using Core.Wrappers;

namespace Core.Features.Students.Queries.Models;

public class GetStudentPaginationListQuery : IRequest<PaginationResult<GetStudentPaginationListResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string[]? Orderby { get; set; }
    public string? Search { get; set; }

}
