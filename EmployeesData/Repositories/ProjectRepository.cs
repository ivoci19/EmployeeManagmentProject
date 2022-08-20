using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            get
            {
                return _applicationDbContext.Projects
                    .Include(p => p.ProjectTasks)
                    .Include(p => p.Users)
                    .Where(p => p.IsActive).ToList();
            }
        }

        public bool DeleteProject(int id)
        {
            Project project = Projects.Where(p => p.Id == id && p.IsActive).FirstOrDefault();
            project.IsActive = false;
            _applicationDbContext.SaveChanges();
            return true;

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
            Project project = Projects.Where(p => p.Id == id && p.IsActive).FirstOrDefault();
            return project;
        }
        /// <summary>
        /// This method returns all the projects that are assigned to an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>IEnumerable<Project></returns>
        public IEnumerable<Project> GetProjectsByUserId(int employeeId)
        {
            IEnumerable<Project> projects = _applicationDbContext.Projects
                .Include(i => i.Users)
                .Where(p => p.Users.Any(proj => proj.Id == employeeId));
            return projects;
        }

        public Project GetProjectByUserId(int employeeId, int projectId)
        {
            IEnumerable<Project> projects = GetProjectsByUserId(employeeId);
            Project project = projects.Where(p => p.Id == projectId).FirstOrDefault();
            return project;
        }

        public Project AddEmployeeToProject(int employeeId, int projectId, User employee, Project project)
        {
            project.Users.Add(employee);
            SaveProject(project);
            return project;
        }
        public Project RemoveEmployeeFromProject(int employeeId, int projectId, User employee)
        {
            Project project = Projects.Where(p => p.Id == projectId).FirstOrDefault();
            project.Users.Remove(employee);
            SaveProject(project);
            return project;
        }
    }
}
