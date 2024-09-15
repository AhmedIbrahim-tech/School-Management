
namespace Core.Features.Students.Queries.Requests;

public class GetStudentPaginationListQuery : IRequest<GenericBaseResponse<PaginationResult<GetStudentPaginationListResponse>>>
{
    const int maxPageSize = 50000;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 100;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }

    public StudentOrderEnum Orderby { get; set; }
    public string? Search { get; set; }

}
