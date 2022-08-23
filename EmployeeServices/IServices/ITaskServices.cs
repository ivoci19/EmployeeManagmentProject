using SharedModels.Enum;
using SharedModels.Models;
using SharedModels.ViewModels;
using System.Collections.Generic;

namespace EmployeeServices.IServices
{
    public interface ITaskServices
    {
        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetAllTasks(UserViewModel user);
        public ApiResponse<ProjectTaskViewModel> GetTaskById(int id, UserViewModel user);
        public ApiResponse<ProjectTaskViewModel> CreateTask(ProjectTaskEditViewModel task, UserViewModel user);
        public ApiResponse<ProjectTaskViewModel> UpdateTask(ProjectTaskEditViewModel taskData, int id, UserViewModel user);
        public ApiResponse<bool> DeleteTask(int id);
        public ApiResponse<ProjectTaskViewModel> ChangeTaskStatus(int taskId, TaskStatusEnum status, UserViewModel user);
        public ApiResponse<ProjectTaskViewModel> AssignTaskToEmployee(int taskId, int employeeId, UserViewModel userVm);
    }
}
