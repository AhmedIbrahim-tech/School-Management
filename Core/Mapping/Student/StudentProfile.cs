
namespace Core.Mapping.Student;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        // Get Student List 
        CreateMap<Data.Entities.Student, GetStudentListResponse>()
            .ForMember(response => response.DepartmentName, options => options.MapFrom(Sour => Sour.Department.DNameEn))
            .ForMember(response => response.Name, options => options.MapFrom(Sour => Sour.GeneralLocalize(Sour.NameAr, Sour.NameEn)));

        //Get Single Student 
        CreateMap<Data.Entities.Student, GetSingleStudentResponse>()
            .ForMember(response => response.DepartmentName, options => options.MapFrom(Sour => Sour.Department.DNameEn))
            .ForMember(response => response.Name, options => options.MapFrom(Sour => Sour.GeneralLocalize(Sour.NameAr, Sour.NameEn)));


        //Add Student 
        CreateMap<AddStudentCommand, Data.Entities.Student>()
            .ForMember(response => response.DepartmentID, options => options.MapFrom(Sour => Sour.DepartmentId))
            .ForMember(response => response.NameAr, options => options.MapFrom(Sour => Sour.NameAr))
            .ForMember(response => response.NameEn, options => options.MapFrom(Sour => Sour.NameEn));


        //Edit Student 
        CreateMap<EditStudentCommand, Data.Entities.Student>()
            .ForMember(response => response.DepartmentID, options => options.MapFrom(Sour => Sour.DepartmentId))
            .ForMember(response => response.StudID, option => option.MapFrom(sour => sour.Id));

        //Delete Student 
        CreateMap<DeleteStudentCommand, Data.Entities.Student>();

    }
}

