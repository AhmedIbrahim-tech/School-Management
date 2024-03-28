namespace Core.Features.Department.Queries.Handlers;

public class DepartmentQueryHandler : GenericBaseResponseHandler, IRequestHandler<GetSingleDepartmentQuery, GenericBaseResponse<GetSingleDepartmentResponse>>
{
    #region Fields
    private readonly IDepartmentServices _departmentService;
    private readonly IStudentServices _studentService;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly IMapper _mapper;
    #endregion

    #region Constructors
    public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IDepartmentServices departmentService,
                                  IMapper mapper,
                                  IStudentServices studentService) : base(stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
        _mapper = mapper;
        _studentService = studentService;
        _departmentService = departmentService;
    }

    #endregion

    public async Task<GenericBaseResponse<GetSingleDepartmentResponse>> Handle(GetSingleDepartmentQuery request, CancellationToken cancellationToken)
    {
        // Async With services
        var result = await _departmentService.GetDepartmentByIdAsync(request.Id);

        // Check If Not Exits
        if (result == null) return NotFound<GetSingleDepartmentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

        // Mapping
        var Mapping = _mapper.Map<GetSingleDepartmentResponse>(result);

        // Pagination
        Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.GeneralLocalize(e.NameAr, e.NameEn));
        var studentQuerable = _studentService.GetStudentsByDepartmentIDQuerable(request.Id);
        var PaginatedList = await studentQuerable.Select(expression).ToPaginationListAsync(request.StudentPageNumber, request.StudentPageSize);
        Mapping.StudentsList = PaginatedList;

        // Return response
        return Success(Mapping);
    }
}
