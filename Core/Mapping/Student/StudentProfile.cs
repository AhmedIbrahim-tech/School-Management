
namespace Core.Mapping.Student;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        // GetStudentListResponse
        CreateMap<Data.Entities.Student, GetStudentListResponse>()
            .ForMember(response => response.DepartmentName, options => options.MapFrom(Sour => Sour.Department.DName));

        //GetSingleStudentResponse
        CreateMap<Data.Entities.Student, GetSingleStudentResponse>()
            .ForMember(response => response.DepartmentName, options => options.MapFrom(Sour => Sour.Department.DName));

        //AddStudentCommand
        CreateMap<AddStudentCommand, Data.Entities.Student>()
            .ForMember(response => response.DepartmentID, options => options.MapFrom(Sour => Sour.DepartmentId));
    }
}

