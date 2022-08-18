using SharedModels.Enum;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.IServices
{
    public interface ITaskServices
    {
        public ProjectTaskViewModel GetTaskById(int id);
        public ProjectTaskViewModel CreateTask(ProjectTaskEditViewModel task);
        public ProjectTaskViewModel UpdateTask(ProjectTaskEditViewModel taskData, int id);
        public bool DeleteTask(int id);
        public IEnumerable<ProjectTaskViewModel> GetAllTasks();
        public ProjectTaskViewModel ChangeTaskStatus(int id, TaskStatusEnum status);
        public ProjectTaskViewModel ChangeTaskStatus(ProjectTaskViewModel taskVm, int TaskId);
        public ProjectTaskViewModel GetTaskByIdAndUserId(int TaskId, int UserId);
        public IEnumerable<ProjectTaskViewModel> GetTasks(int UserId);
    }
}
