namespace Core.Pagination;

public class PaginationResult<T>
{
    public List<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public object Meta { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public List<string> Messages { get; set; } = new();
    public bool Succeeded { get; set; }

    private PaginationResult(bool succeeded, List<T> data = null, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
    {
        Succeeded = succeeded;
        Data = data ?? new List<T>();
        Messages = messages ?? new List<string>();
        TotalCount = count;
        CurrentPage = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public static PaginationResult<T> Success(List<T> data, int count, int page, int pageSize)
    {
        return new PaginationResult<T>(true, data, null, count, page, pageSize);
    }

    public static PaginationResult<T> Failure(List<string> messages = null)
    {
        return new PaginationResult<T>(false, null, messages ?? new List<string>());
    }
}
