namespace Core.Wrappers;

public class PaginationResult<T>
{
    public List<T> Data { get; set; }
    public PaginationResult(List<T> data)
    {
        this.Data = data;
    }

    public PaginationResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pagesize = 10)
    {
        this.Data = data;
        this.CurrentPage = page;
        this.Succeeded = succeeded;
        this.PageSize = pagesize;
        this.TotalPages = (int)Math.Ceiling(count / (double)this.PageSize);
        this.TotalCount = count;
    }

    public static PaginationResult<T> Success(List<T> data, int count, int page, int pagesize)
    {
        return new(true, data, null, count, page, pagesize);
    }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public object Meta { get; set; }
    public int PageSize { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public List<string> Messages { get; set; } = new();
    public bool Succeeded { get; set; }

}
