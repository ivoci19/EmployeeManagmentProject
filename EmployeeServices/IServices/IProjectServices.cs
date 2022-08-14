using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.IServices
{
    public interface IProjectServices
    {
        public ProjectViewModel GetProjectById(int id);
        public ProjectViewModel CreateProject(ProjectViewModel project);
        public ProjectViewModel UpdateProject(ProjectViewModel project);
        public bool DeleteProject(ProjectViewModel project);
        public List<ProjectViewModel> GetAllProjects();

    }
}
