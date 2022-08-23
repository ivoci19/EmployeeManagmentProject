using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Enum;
using System;
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

        public IQueryable<Project> Projects
        {
            get
            {
                return _applicationDbContext.Projects
                    .Include(p => p.ProjectTasks.Where(i => i.IsActive))
                    .Include(p => p.Users.Where(i => i.IsActive))
                    .Where(p => p.IsActive);
            }
        }

        public bool DeleteProject(int projectId)
        {
            Project project = Projects.Where(p => p.Id == projectId).FirstOrDefault();
            //Soft Delete
            project.IsActive = false;
            //I have added the Guid text next to the Code in order to not have duplicated values
            project.Code = project.Code + "_" + Guid.NewGuid();
            _applicationDbContext.SaveChanges();
            return true;

        }

        public void SaveProject(Project project)
        {
            //When a new project is created
            if (project.Id == 0)
            {
                project.IsActive = true;
                _applicationDbContext.Projects.Add(project);
            }
            _applicationDbContext.SaveChanges();
        }

        public Project GetProjectById(int projectId)
        {
            Project project = Projects.Where(p => p.Id == projectId).FirstOrDefault();
            return project;
        }

        //This method returns the projects that an employee is assigned to
        public IEnumerable<Project> GetProjectsByUserId(int employeeId)
        {
            IEnumerable<Project> projects = _applicationDbContext.Projects
                .Include(i => i.Users)
                .Where(p => p.Users.Any(proj => proj.Id == employeeId));
            return projects;
        }

        //This method returns the project details by employeeId and projectId
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
        //It returns true if the project has open tasks and it returns false otherwise
        public bool HasOpenProjectTasks(int projectId)
        {
            var projectsHasOpenTasks = Projects.Where(i => i.Id == projectId).FirstOrDefault().ProjectTasks.Any(i => i.TaskStatus != TaskStatusEnum.DONE);
            return projectsHasOpenTasks;
        }

        public bool IsCodeUsed(string code, int projectId, bool isUpdate)
        {
            //get user list with the code
            var project = Projects.Where(i => i.Code == code).ToList();
            //get the project which will be updated
            var projectToUpdate = Projects.Where(i => i.Code == code && i.Id == projectId).FirstOrDefault();
            //if we don't have any project in the database with the same code
            //and the isUpdate is false then the code is not used
            if (project.Count == 0 && !isUpdate)
                return false;
            //if isUpdate is true and the count of project which have the same code is greater than 0
            //and the codeToUpdate is null then we have duplicated code
            if (isUpdate && project.Count > 0 && projectToUpdate == null)
                return true;
            //if isUpdate is true
            if (isUpdate)
                return false;
            return true;
        }
    }
}
