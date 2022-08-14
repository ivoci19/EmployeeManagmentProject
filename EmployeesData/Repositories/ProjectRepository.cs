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
        public List<Project> Projects
        {
            get { return _applicationDbContext.Projects.ToList(); }
        }
    }
}
