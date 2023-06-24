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
        var MapperObj = _mapper.Map<List<GetStudentListResponse>>(response);
        return Success(MapperObj);
    }

    public async Task<GenericBaseResponse<GetSingleStudentResponse>> Handle(GetSingleStudentQuery request, CancellationToken cancellationToken)
    {
        var response = await _studentServices.GetStudentsByIdAsync(request.Id);
        if (response == null) return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
        var MapperObj = _mapper.Map<GetSingleStudentResponse>(response);
        return Success(MapperObj);

    }

    public Task<PaginationResult<GetStudentPaginationListResponse>> Handle(GetStudentPaginationListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Student, GetStudentPaginationListResponse>> expression = e => new GetStudentPaginationListResponse(e.StudID, e.NameEn, e.Address, e.Phone, e.Department.DNameEn);
        var FilterQuery = _studentServices.FilterStudentsPaginationQueryAbleAsync(request.Orderby, request.Search);
        var PaginationList = FilterQuery.Select(expression).ToPaginationListAsync(request.PageNumber, request.PageSize);
        return PaginationList;
    }
    #endregion
}
