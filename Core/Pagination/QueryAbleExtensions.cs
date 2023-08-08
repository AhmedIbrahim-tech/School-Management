namespace Core.Pagination;

public static class QueryAbleExtensions
{
    public static async Task<PaginationResult<T>> ToPaginationListAsync<T>(this IQueryable<T> sourse, int pagenumber, int pagesize) where T : class
    {
        if (sourse == null)
        {
            throw new Exception("");
        }
        pagenumber = pagenumber == 0 ? 1 : pagenumber;
        pagesize = pagesize == 0 ? 10 : pagesize;
        int count = await sourse.AsNoTracking().CountAsync();
        if (count == 0) { return PaginationResult<T>.Success(new List<T>(), count, pagesize, pagenumber); }
        pagenumber = pagenumber <= 0 ? 1 : pagenumber;
        var items = await sourse.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
        return PaginationResult<T>.Success(items, count, pagesize, pagenumber);
    }
}

