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
            get { return _applicationDbContext.Projects.Where(i => i.IsActive).ToList(); }
        }

        public bool DeleteProject(int id)
        {
            Project project = Projects.Where(i => i.Id == id && i.IsActive).FirstOrDefault();

            if (project != null)
            {
                project.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void SaveProject(Project project)
        {
            if (project.Id == 0)
            {
                project.CreatedBy = 1; 
                project.IsActive = true;
                _applicationDbContext.Projects.Add(project);
            }
            project.UpdatedBy = 1;
            _applicationDbContext.SaveChanges();
        }
        public Project GetProjectById(int id)
        {
            Project project = Projects.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
            return project;
        }
    }
}
