using Data.Command;
using Data.Entities.Models;

namespace Core.Features.Students.Queries.Handlers;

public class StudentQueryHandler : GenericBaseResponseHandler,
                                   IRequestHandler<GetStudentListQuery, GenericBaseResponse<List<GetStudentListResponse>>>,
                                   IRequestHandler<GetSingleStudentQuery, GenericBaseResponse<GetSingleStudentResponse>>,
                                   IRequestHandler<GetStudentPaginationListQuery, PaginationResult<GetStudentPaginationListResponse>>
{
    #region Fields
    private readonly IStudentServices _studentServices;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    #endregion

    #region Contractor
    public StudentQueryHandler(IStudentServices studentServices, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
    {
        _studentServices = studentServices;
        _mapper = mapper;
        _stringLocalizer = stringLocalizer;
    }
    #endregion

    #region Handler Function
    public async Task<GenericBaseResponse<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var response = await _studentServices.GetStudentsListAsync();
        var mapperObj = _mapper.Map<List<GetStudentListResponse>>(response);
        var result = Success(mapperObj);
        result.Meta = new { Count = result.Data.Count() };
        return result;
    }

    public async Task<GenericBaseResponse<GetSingleStudentResponse>> Handle(GetSingleStudentQuery request, CancellationToken cancellationToken)
    {
        var response = await _studentServices.GetStudentsByIdAsync(request.Id);
        var mapperObj = _mapper.Map<GetSingleStudentResponse>(response);
        return Success(mapperObj);

    }

    public async Task<PaginationResult<GetStudentPaginationListResponse>> Handle(GetStudentPaginationListQuery request, CancellationToken cancellationToken)
    {
        //Expression<Func<Student, GetStudentPaginationListResponse>> expression = e => new GetStudentPaginationListResponse(e.StudID, GeneralLocalizeEntity.GeneralLocalize(e.NameAr, e.NameEn), e.Address, e.Phone, GeneralLocalizeEntity.GeneralLocalize(e.Department.DNameAr, e.Department.DNameEn));
        // var FilterQuery = _studentServices.FilterStudentsPaginationQueryAbleAsync(request.Orderby, request.Search);
        // var PaginationList = await FilterQuery.Select(expression).ToPaginationListAsync(request.PageNumber, request.PageSize);
        // PaginationList.Meta = new { Count = PaginationList.Data.Count() };
        // return PaginationList;
        
        
        var filterQuery = _studentServices.FilterStudentsPaginationQueryAbleAsync(request.Orderby, request.Search);
        var paginatedList = await _mapper.ProjectTo<GetStudentPaginationListResponse>(filterQuery).ToPaginationListAsync(request.PageNumber, request.PageSize);
        paginatedList.Meta=new { Count = paginatedList.Data.Count() };
        return paginatedList;

    }
    #endregion
}
