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
        public ProjectViewModel CreateProject(ProjectEditViewModel project);
        public ProjectViewModel UpdateProject(ProjectEditViewModel projectData, int id);
        public bool DeleteProject(int id);
        public IEnumerable<ProjectViewModel> GetAllProjects();
        public IEnumerable<ProjectViewModel> GetEmployeeProjects(int employeeId);
        public bool AddEmployeeToProject(int employeeId, int projectId);
        public bool RemoveEmployeeFromProject(int employeeId, int projectId);

    }
}
