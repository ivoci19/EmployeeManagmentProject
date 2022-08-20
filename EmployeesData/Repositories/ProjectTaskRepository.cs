using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Enum;
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
        public List<ProjectTask> ProjectTasks
        {
            get
            {
                return _applicationDbContext.ProjectTasks
                                              .Include(i => i.Project)
                                              .Include(i => i.User)
                                              .Where(i => i.IsActive)
                                              .ToList();
            }
        }

        public ProjectTask GetTaskById(int id)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
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

        public bool DeleteTask(int id)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
            projectTask.IsActive = false;
            _applicationDbContext.SaveChanges();
            return true;
        }

        public ProjectTask GetTaskByIdAndUserId(int id, int UserId)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == id && i.IsActive && i.AssignedTo == UserId).FirstOrDefault();
            return projectTask;
        }
        public IEnumerable<ProjectTask> GetTasksByUserId(int UserId)
        {
            IEnumerable<ProjectTask> projectTask = ProjectTasks.Where(i => i.IsActive && i.AssignedTo == UserId);
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
        public bool IsTaskStatusDone(int taskId)
        {
            ProjectTask task = ProjectTasks.Where(i => i.Id == taskId).FirstOrDefault();
            if (task.TaskStatus == TaskStatusEnum.DONE)
                return false;
            else
                return true;
        }
    }
}
