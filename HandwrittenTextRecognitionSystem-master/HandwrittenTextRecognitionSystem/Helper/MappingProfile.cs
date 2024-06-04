using AutoMapper;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group, GroupDto>()
                    .ForMember(d => d.Teacher, o => o.MapFrom(s => s.Teacher.ApplicationUser.FirstName));
            CreateMap<Post, PostDto>()
                    .ForMember(d => d.Teacher, o => o.MapFrom(s => s.Teacher.ApplicationUser.FirstName));
            CreateMap<Solution, SolutionDto>()
                    .ForMember(d => d.Assignment, o => o.MapFrom(s => s.Assignment.Name));
            CreateMap<Student, StudentDto>()
                    .ForMember(d => d.Department, o => o.MapFrom(s => s.Department.Name))
                    .ForMember(d => d.ApplicationUser, o => o.MapFrom(s => s.ApplicationUser.FirstName));
            CreateMap<Teacher, TeacherDto>()
                    .ForMember(d => d.Department, o => o.MapFrom(s => s.Department.Name))
                    .ForMember(d => d.ApplicationUser, o => o.MapFrom(s => s.ApplicationUser.FirstName));
            CreateMap<Assignment, AssignmentDto>()
                    .ForMember(d => d.Course, o => o.MapFrom(s => s.Course.Name))
                    .ForMember(d => d.Teacher, o => o.MapFrom(s => s.Teacher.ApplicationUser.FirstName))
                    .ForMember(d => d.Path, o => o.MapFrom<AssignmentPictureUrlResolver>());
            CreateMap<CreateAndEditAssigmentDto, Assignment>();
            CreateMap<Course, CourseDto>();
            CreateMap<Assignment, ViewAssigmentDto>();
            CreateMap<Department, ViewDepartmentDto>();
            CreateMap<Request, ViewRequstDto>().ForMember(d => d.studentName, o => o.MapFrom(s => s.Student.Name));

        }
    }
    }

