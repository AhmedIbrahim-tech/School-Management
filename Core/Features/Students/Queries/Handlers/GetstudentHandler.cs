namespace Core.Features.Students.Queries.Handlers;

public class GetstudentHandler : IRequestHandler<GetStudentListQuery, List<Student>>
{
    #region Fileds
    private readonly IStudentServices _studentServices;
    #endregion

    #region Constractor
    public GetstudentHandler(IStudentServices studentServices)
    {
        this._studentServices = studentServices;
    }
    #endregion

    #region Handler Function
    public async Task<List<Student>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        return await _studentServices.GetStudentsListAsync();
    }
    #endregion
}
