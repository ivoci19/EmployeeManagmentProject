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
        public bool DeleteProject(int projectId);
    }
}
