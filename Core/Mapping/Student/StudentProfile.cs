using AutoMapper;
using Data.Command;

namespace Core.Mapping.Student;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        // Get Student List 
        MapStudentList();
        
        // Get Single Student 
        MapSingleStudent();
        
        // Add Student 
        MapAddStudent();
        
        // Edit Student 
        MapEditStudent();
        
        // Delete Student 
        MapDeleteStudent();
    }

    private void MapStudentList()
    {
        // Get Student List 
        CreateMap<Data.Entities.Models.Student, GetStudentListResponse>()
            .ForMember(destination => destination.DepartmentName, options => 
                options.MapFrom(sour => sour.Department != null ? sour.Department.DNameEn : string.Empty)) // Handle null Department
            .ForMember(destination => destination.Name, options => 
                options.MapFrom(sour => GeneralLocalizeEntity.GeneralLocalize(sour.NameAr, sour.NameEn)))
            .ReverseMap();
    }
    
    private void MapSingleStudent()
    {
        // Get Single Student 
        CreateMap<Data.Entities.Models.Student, GetSingleStudentResponse>()
            .ForMember(destination => destination.DepartmentName, options => 
                options.MapFrom(sour => sour.Department != null ? sour.Department.DNameEn : string.Empty)) // Handle null Department
            .ForMember(destination => destination.Name, options => 
                options.MapFrom(sour => GeneralLocalizeEntity.GeneralLocalize(sour.NameAr, sour.NameEn)))
            .ReverseMap();
    }
    
    private void MapAddStudent()
    {
        // Add Student 
        CreateMap<AddStudentCommand, Data.Entities.Models.Student>()
            .ForMember(destination => destination.DID, options => 
                options.MapFrom(sour => sour.DepartmentId))
            .ForMember(destination => destination.NameAr, options => 
                options.MapFrom(sour => sour.NameAr))
            .ForMember(destination => destination.NameEn, options => 
                options.MapFrom(sour => sour.NameEn))
            .ReverseMap();
    }
    
    
    private void MapEditStudent()
    {
        // Edit Student 
        CreateMap<EditStudentCommand, Data.Entities.Models.Student>()
            .ForMember(destination => destination.DID, options => 
                options.MapFrom(sour => sour.DepartmentId))
            .ForMember(destination => destination.StudID, options => 
                options.MapFrom(sour => sour.Id));
    }

    
    private void MapDeleteStudent()
    {
        // Delete Student 
        CreateMap<DeleteStudentCommand, Data.Entities.Models.Student>();
    }
}
