using Data.Entities;

namespace Core.Features.Students.Commands.Handlers;

public class StudentCommandHandler : GenericBaseResponseHandler,
    IRequestHandler<AddStudentCommand, GenericBaseResponse<int>>,
    IRequestHandler<EditStudentCommand, GenericBaseResponse<int>>

{

    #region Fields
    private readonly IStudentServices _studentServices;
    private readonly IMapper _mapper;
    #endregion

    #region Contractor (s)
    public StudentCommandHandler(IStudentServices studentServices, IMapper mapper)
    {
        this._studentServices = studentServices;
        this._mapper = mapper;
    }
    #endregion

    #region Handler Function

    public async Task<GenericBaseResponse<int>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        // Mapping
        var Mapper = _mapper.Map<Student>(request);

        // Added
        var response = await _studentServices.AddAsync(Mapper);

        // Check Condition
        //if (response == "Exist") return AlreadyExit<string>();

        // return response
        if (response == 1) return Created<int>(response);
        else return NotFound<int>();
    }

    public async Task<GenericBaseResponse<int>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        // Check if this's Id Is Exist
        var CurrentStudent = await _studentServices.GetStudentsByIdAsync(request.Id);

        // Check If Return Not Null
        if (CurrentStudent == null) return NotFound<int>();

        // Mapping
        var Mapper = _mapper.Map<Student>(request);

        // Added
        var response = await _studentServices.EditAsync(Mapper);

        // return response
        if (response == 1) return Updated<int>(response);
        else return NotFound<int>();

    }

    #endregion
}
