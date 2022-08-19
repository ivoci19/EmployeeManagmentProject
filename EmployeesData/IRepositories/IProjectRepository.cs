using EmployeesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.IRepositories
{
    public interface IProjectRepository
    {
        List<Project> Projects { get; }
        public void SaveProject(Project project);
        public bool DeleteProject(int id);
        public Project GetProjectById(int id);
        public IEnumerable<Project> GetProjectsByUserId(int employeeId);
        public Project AddEmployeeToProject(int employeeId, int projectId, User user);
        public Project RemoveEmployeeFromProject(int employeeId, int projectId, User user);
    }
}
