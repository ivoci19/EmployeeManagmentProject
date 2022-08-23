using EmployeesData.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesData.IRepositories
{
    public interface IProjectRepository
    {
        IQueryable<Project> Projects { get; }
        public void SaveProject(Project project);
        public bool DeleteProject(int projectId);
        public Project GetProjectById(int projectId);
        public IEnumerable<Project> GetProjectsByUserId(int employeeId);
        public Project GetProjectByUserId(int employeeId, int projectId);
        public Project AddEmployeeToProject(int employeeId, int projectId, User employee, Project project);
        public Project RemoveEmployeeFromProject(int employeeId, int projectId, User employee);
        public bool HasOpenProjectTasks(int projectId);
        public bool IsCodeUsed(string code, int projectId, bool isUpdate);
    }
}
