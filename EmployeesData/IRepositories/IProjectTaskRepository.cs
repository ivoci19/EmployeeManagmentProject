using EmployeesData.Models;
using System.Collections.Generic;

namespace EmployeesData.IRepositories
{
    public interface IProjectTaskRepository
    {
        List<ProjectTask> ProjectTasks { get; }
        public void SaveTask(ProjectTask projectTask);
        public bool DeleteTask(int id);
        public ProjectTask GetTaskById(int id);
        public ProjectTask GetTaskByIdAndUserId(int id, int UserId);
        public IEnumerable<ProjectTask> GetTasksByUserId(int UserId);
        public IEnumerable<ProjectTask> GetTasksByProjectId(int projectId);
        public IEnumerable<ProjectTask> GetTasksOfUserProjects(IEnumerable<Project> projects);
        public bool IsTaskStatusDone(int taskId);
    }
}
