namespace Core.Features.Students.Queries.Handlers;

public class GetstudentHandler : GenericBaseResponseHandler , IRequestHandler<GetStudentListQuery, GenericBaseResponse<List<GetStudentListResponse>>>
{
    #region Fileds
    private readonly IStudentServices _studentServices;
    private readonly IMapper _mapper;
    #endregion

    #region Constractor
    public GetstudentHandler(IStudentServices studentServices, IMapper mapper)
    {
        this._studentServices = studentServices;
        this._mapper = mapper;
    }
    #endregion

    #region Handler Function
    public async Task<GenericBaseResponse<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var response = await _studentServices.GetStudentsListAsync();
        var MapperObj = _mapper.Map<List<GetStudentListResponse>>(response);
        return Success(MapperObj);
    }
    #endregion
}
