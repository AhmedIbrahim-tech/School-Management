namespace Core.Mapping.Student;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Data.Entities.Student, GetStudentListResponse>()
            .ForMember(response => response.DepartmentName, options => options.MapFrom(Sour => Sour.Department.DName));

        CreateMap<Data.Entities.Student, GetSingleStudentResponse>()
            .ForMember(response => response.DepartmentName, options => options.MapFrom(Sour => Sour.Department.DName));
    }
}

