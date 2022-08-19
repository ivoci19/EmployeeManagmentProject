using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
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
            get { return _applicationDbContext.Projects
                    .Include(i => i.ProjectTasks)
                    .Include(i => i.Users)
                    .Where(i => i.IsActive).ToList(); }
        }

        public bool DeleteProject(int id)
        {
            Project project = Projects.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
            //Nese projekti ka taske te hapura (dmth ne enum pending dhe in progress nuk mund te fshihet)
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
                project.IsActive = true;
                _applicationDbContext.Projects.Add(project);
            }
            _applicationDbContext.SaveChanges();
        }

        public Project GetProjectById(int id)
        {
            Project project = Projects.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
            return project;
        }

        public IEnumerable<Project> GetProjectsByUserId(int employeeId)
        {
            IEnumerable<Project> projects = _applicationDbContext.Projects
                .Include(i => i.Users)
                .Where(p => p.Users.Any(proj => proj.Id == employeeId));
            return projects;
        }

        public Project AddEmployeeToProject(int employeeId, int projectId, User user)
        {
            Project project = Projects.Where(i => i.Id == projectId).FirstOrDefault();
            project.Users.Add(user);
            SaveProject(project);
            return project;
        }
        public Project RemoveEmployeeFromProject(int employeeId, int projectId, User user)
        {
            Project project = Projects.Where(i => i.Id == projectId).FirstOrDefault();
            project.Users.Remove(user);
            SaveProject(project);
            return project;
        }

    }
}
