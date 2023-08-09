using Core.Features.Department.Queries.Models;
using Services.Interface;

namespace Core.Features.Department.Queries.Handlers;

public class DepartmentQueryHandler : GenericBaseResponseHandler, IRequestHandler<GetSingleDepartmentQuery, GenericBaseResponse<GetSingleDepartmentResponse>>
{
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly IDepartmentServices _departmentServices;

    public DepartmentQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, IDepartmentServices departmentServices) : base(stringLocalizer)
    {
        this._mapper = mapper;
        this._stringLocalizer = stringLocalizer;
        this._departmentServices = departmentServices;
    }

    public async Task<GenericBaseResponse<GetSingleDepartmentResponse>> Handle(GetSingleDepartmentQuery request, CancellationToken cancellationToken)
    {
        // Async With services
        var result = await _departmentServices.GetDepartmentByIdAsync(request.Id);

        // Check If Not Exits
        if (result == null) return NotFound<GetSingleDepartmentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

        // Mapping
        var Mapping = _mapper.Map<GetSingleDepartmentResponse>(result);

        // Pagination
        Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.GeneralLocalize(e.NameAr, e.NameEn));
        var FilterQuery = _departmentServices.GetStudentByDepartmentIDAbleAsync(request.Id);
        var PaginationList = await FilterQuery.Select(expression).ToPaginationListAsync(request.StudentPageNumber, request.StudentPageSize);
        PaginationList.Meta = new { Count = PaginationList.Data.Count() };

        // Return response
        return Success(Mapping);
    }
}
