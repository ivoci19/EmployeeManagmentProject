using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesData.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProjectTaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IQueryable<ProjectTask> ProjectTasks
        {
            get
            {
                return _applicationDbContext.ProjectTasks
                                              .Include(i => i.Project)
                                              .Include(i => i.User)
                                              .Where(i => i.IsActive);
            }
        }

        public ProjectTask GetTaskById(int taskId)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == taskId).FirstOrDefault();
            return projectTask;
        }

        public void SaveTask(ProjectTask projectTask)
        {
            if (projectTask.Id == 0)
            {
                projectTask.IsActive = true;
                _applicationDbContext.ProjectTasks.Add(projectTask);
            }
            _applicationDbContext.SaveChanges();
        }

        public bool DeleteTask(int taskId)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == taskId).FirstOrDefault();
            projectTask.IsActive = false;
            _applicationDbContext.SaveChanges();
            return true;
        }

        public ProjectTask GetTaskByIdAndUserId(int id, int userId)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == id && i.AssignedTo == userId).FirstOrDefault();
            return projectTask;
        }
        public IEnumerable<ProjectTask> GetTasksByUserId(int userId)
        {
            IEnumerable<ProjectTask> projectTask = ProjectTasks.Where(i => i.AssignedTo == userId);
            return projectTask;
        }

        public IEnumerable<ProjectTask> GetTasksByProjectId(int projectId)
        {
            IEnumerable<ProjectTask> tasks = ProjectTasks.Where(i => i.Project.Id == projectId);
            return tasks;
        }

        public IEnumerable<ProjectTask> GetTasksOfUserProjects(IEnumerable<Project> projects)
        {
            IEnumerable<ProjectTask> projectTask = null;
            foreach (Project project in projects)
            {
                projectTask = project.ProjectTasks;
            }
            return projectTask;
        }
    }
}
