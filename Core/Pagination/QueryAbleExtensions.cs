namespace Core.Pagination;

public static class QueryAbleExtensions
{
    public static async Task<PaginationResult<T>> ToPaginationListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source), "Source query cannot be null.");
        }

        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int totalCount = await source.AsNoTracking().CountAsync();

        if (totalCount == 0)
        {
            return PaginationResult<T>.Success(new List<T>(), totalCount, pageNumber, pageSize);
        }

        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return PaginationResult<T>.Success(items, totalCount, pageNumber, pageSize);
    }
}

