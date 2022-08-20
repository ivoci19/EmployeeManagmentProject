using AutoMapper;
using EmployeesData.Models;
using SharedModels.ViewModels;

namespace EmployeeServices.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserEditViewModel, User>();
            CreateMap<User, UserViewModel>()
               .ForPath(dest => dest.RoleName, act => act.MapFrom(src => src.Role.RoleName));

            CreateMap<Project, ProjectViewModel>();
            CreateMap<ProjectEditViewModel, Project>();

            CreateMap<ProjectTask, ProjectTaskViewModel>()
                .ForPath(dest => dest.Username, act => act.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.ProjectName, act => act.MapFrom(src => src.Project.Name));
            CreateMap<ProjectTaskEditViewModel, ProjectTask>();
            CreateMap<ProjectTaskViewModel, ProjectTask>();


            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleEditViewModel, Role>();
        }
    }
}
