using EmployeesData.IRepositories;
using EmployeesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProjectRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<Project> Projects => throw new NotImplementedException();

        public bool DeleteProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public void SaveProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
