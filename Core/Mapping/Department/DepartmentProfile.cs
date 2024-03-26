using Data.Entities.Models;

namespace Core.Mapping.Department;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        //Get Single
        CreateMap<Data.Entities.Models.Department, GetSingleDepartmentResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GeneralLocalize(src.DNameAr, src.DNameEn)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.GeneralLocalize(src.Instructor.ENameAr, src.Instructor.ENameEn)))

            .ForMember(dest => dest.SubjectsList, opt => opt.MapFrom(src => src.DepartmentSubjects))
            //.ForMember(dest => dest.StudentsList, opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.InstructorsList, opt => opt.MapFrom(src => src.Instructors));

        CreateMap<DepartmetSubject, SubjectResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.GeneralLocalize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

        //CreateMap<Student, StudentResponse>()
        //     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
        //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GeneralLocalize(src.NameAr, src.NameEn)));

        CreateMap<Instructor, InstructorResponse>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GeneralLocalize(src.ENameAr, src.ENameEn)));


    }
}
