using EmployeesData.Models;
using System.Collections.Generic;

namespace EmployeesData.IRepositories
{
    public interface IProjectRepository
    {
        List<Project> Projects { get; }
        public void SaveProject(Project project);
        public bool DeleteProject(int id);
        public Project GetProjectById(int id);
        public IEnumerable<Project> GetProjectsByUserId(int employeeId);
        public Project GetProjectByUserId(int employeeId, int projectId);
        public Project AddEmployeeToProject(int employeeId, int projectId, User employee, Project project);
        public Project RemoveEmployeeFromProject(int employeeId, int projectId, User user);
    }
}
