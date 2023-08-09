namespace Core.Mapping.Department;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        //Get Single
        CreateMap<Data.Entities.Department, GetSingleDepartmentResponse>()
            .ForMember(response => response.Name, options =>
                                   options.MapFrom(Sour => Sour.GeneralLocalize(Sour.DNameAr, Sour.DNameEn)))
            .ForMember(response => response.Id, options => options.MapFrom(Sour => Sour.DID))
            .ForMember(response => response.ManagerName, options =>
                                   options.MapFrom(Sour => Sour.GeneralLocalize(Sour.Instructor.NameAr, Sour.Instructor.NameEn)))

            .ForMember(response => response.SubjectsList, options => options.MapFrom(Sour => Sour.DepartmentSubjects))
            .ForMember(response => response.StudentsList, options => options.MapFrom(Sour => Sour.Students))
            .ForMember(response => response.InstructorsList, options => options.MapFrom(Sour => Sour.Instructors))
        .ReverseMap();


        CreateMap<Data.Entities.DepartmetSubject, SubjectResponse>()
            .ForMember(x => x.Id, options => options.MapFrom(sour => sour.SubID))
            .ForMember(x => x.Name, options => options.MapFrom(Sour => Sour.Subject.GeneralLocalize(Sour.Subject.SubjectNameAr, Sour.Subject.SubjectNameEn)));

        CreateMap<Data.Entities.Student, StudentResponse>()
            .ForMember(x => x.Id, options => options.MapFrom(sour => sour.StudID))
            .ForMember(x => x.Name, options => options.MapFrom(Sour => Sour.GeneralLocalize(Sour.NameAr, Sour.NameEn)));

        CreateMap<Data.Entities.Instructor, SubjectResponse>()
            .ForMember(x => x.Id, options => options.MapFrom(sour => sour.InsId))
            .ForMember(x => x.Name, options => options.MapFrom(Sour => Sour.GeneralLocalize(Sour.NameAr, Sour.NameEn)));


        ;
    }
}
