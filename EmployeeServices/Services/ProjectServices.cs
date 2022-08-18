using AutoMapper;
using EmployeesData.IRepositories;
using EmployeesData.Models;
using EmployeeServices.IServices;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectServices(IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ProjectViewModel CreateProject(ProjectEditViewModel projectVm)
        {
            Project project = _mapper.Map<Project>(projectVm);
            _projectRepository.SaveProject(project);
            return _mapper.Map<ProjectViewModel>(project);
        }

        public bool DeleteProject(int id)
        {
            Project project = _projectRepository.Projects.FirstOrDefault(u => u.Id == id);
            
            if (project != null)
            {
                return _projectRepository.DeleteProject(id);
            }
            return false;
        }

        public IEnumerable<ProjectViewModel> GetAllProjects()
        {
            var project = _projectRepository.Projects;

            return _mapper.Map<IEnumerable<ProjectViewModel>>(project); ;
        }

        public ProjectViewModel GetProjectById(int id)
        {
            Project project = _projectRepository.GetProjectById(id);
            return _mapper.Map<ProjectViewModel>(project);
        }

        public ProjectViewModel UpdateProject(ProjectEditViewModel projectData, int id)
        {
            Project project = _projectRepository.Projects.FirstOrDefault(e => e.Id == id);

            if (project != null)
            {
                _mapper.Map(projectData, project);
                _projectRepository.SaveProject(project);
                var projectVm = _mapper.Map<ProjectViewModel>(project);
                return projectVm;
            }

            return null;
        }

        public IEnumerable<ProjectViewModel> GetEmployeeProjects(int userId)
        {
            IEnumerable<Project> projects = _projectRepository.GetProjectsByUserId(userId);
            return _mapper.Map<IEnumerable<ProjectViewModel>>(projects);
        }
    }
}
