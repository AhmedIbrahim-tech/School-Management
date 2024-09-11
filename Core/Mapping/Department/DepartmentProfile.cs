using Data.Command;
using Data.Entities.Models;

namespace Core.Mapping.Department;

    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // Mapping for single department response
            MapSingleDepartment();
            
            // Mapping for department subjects
            MapDepartmentSubjects();
            
            // Mapping for instructors in department
            MapInstructors();
            
            // Uncomment if you want to map students in department
            // MapStudents();
        }

        private void MapSingleDepartment()
        {
            CreateMap<Data.Entities.Models.Department, GetSingleDepartmentResponse>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => GeneralLocalizeEntity.GeneralLocalize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.ManagerName, opt =>
                    opt.MapFrom(src => GeneralLocalizeEntity.GeneralLocalize(src.Instructor != null ? src.Instructor.ENameAr : string.Empty, 
                        src.Instructor != null ? src.Instructor.ENameEn : string.Empty)))
                .ForMember(dest => dest.SubjectsList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(dest => dest.InstructorsList, opt => opt.MapFrom(src => src.Instructors));
        }

        private void MapDepartmentSubjects()
        {
            CreateMap<DepartmetSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => GeneralLocalizeEntity.GeneralLocalize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));
        }

        private void MapInstructors()
        {
            CreateMap<Instructor, InstructorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => GeneralLocalizeEntity.GeneralLocalize(src.ENameAr, src.ENameEn)));
        }

        // Uncomment this method if you need to map students in the department
        /*
        private void MapStudents()
        {
            CreateMap<Student, StudentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Name, opt => 
                    opt.MapFrom(src => GeneralLocalizeEntity.GeneralLocalize(src.NameAr, src.NameEn)));
        }
        */
    }
