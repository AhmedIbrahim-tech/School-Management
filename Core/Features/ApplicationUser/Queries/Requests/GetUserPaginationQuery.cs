namespace Core.Features.ApplicationUser.Queries.Requests
{
    public class GetUserPaginationQuery : IRequest<PaginationResult<GetUserPaginationReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
