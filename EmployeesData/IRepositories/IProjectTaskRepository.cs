using EmployeesData.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesData.IRepositories
{
    public interface IProjectTaskRepository
    {
        IQueryable<ProjectTask> ProjectTasks { get; }
        public void SaveTask(ProjectTask projectTask);
        public bool DeleteTask(int taskId);
        public ProjectTask GetTaskById(int taskId);
        public ProjectTask GetTaskByIdAndUserId(int taskId, int userId);
        public IEnumerable<ProjectTask> GetTasksByUserId(int userId);
        public IEnumerable<ProjectTask> GetTasksByProjectId(int projectId);
        public IEnumerable<ProjectTask> GetTasksOfUserProjects(IEnumerable<Project> projects);
    }
}
