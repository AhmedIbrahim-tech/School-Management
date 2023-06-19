using Core.Features.Students.Commands.Models;
using Data.Entities;

namespace Core.Features.Students.Commands.Handlers;

public class StudentCommandHandler : GenericBaseResponseHandler , IRequestHandler<AddStudentCommand, GenericBaseResponse<string>>
{

    #region Fields
    private readonly IStudentServices _studentServices;
    private readonly IMapper _mapper;
    #endregion

    #region Contractor
    public StudentCommandHandler(IStudentServices studentServices, IMapper mapper)
    {
        this._studentServices = studentServices;
        this._mapper = mapper;
    }
    #endregion

    #region Handler Function

    public async Task<GenericBaseResponse<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        // Mapping
        var Mapper = _mapper.Map<Student>(request);
        
        // Added
        var response = await _studentServices.AddAsync(Mapper);

        // Check Condition
        if (response == "Exist") return AlreadyExit<string>();
        else if (response == "Successfully") return Created<string>(response);
        else return NotFound<string>();


    }

    #endregion
}
