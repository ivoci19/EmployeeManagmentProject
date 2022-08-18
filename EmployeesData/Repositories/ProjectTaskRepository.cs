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
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProjectTaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public List<ProjectTask> ProjectTasks
        {
            get { return _applicationDbContext.ProjectTasks.Include(i => i.Project).Include(i => i.User).Where(i => i.IsActive).ToList(); }
        }

        public bool DeleteTask(int id)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == id && i.IsActive).FirstOrDefault();

            if (projectTask != null)
            {
                projectTask.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
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
        public ProjectTask GetTaskById(int id)
        {
            ProjectTask projectTask = ProjectTasks.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
            return projectTask;
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
    }
}
