using AutoMapper;
using EmployeesData.IRepositories;
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
        private readonly IMapper _mapper;

        public ProjectServices(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public ProjectViewModel CreateProject(ProjectViewModel project)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProject(ProjectViewModel project)
        {
            throw new NotImplementedException();
        }

        public List<ProjectViewModel> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel GetProjectById(int id)
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel UpdateProject(ProjectViewModel project)
        {
            throw new NotImplementedException();
        }
    }
}
