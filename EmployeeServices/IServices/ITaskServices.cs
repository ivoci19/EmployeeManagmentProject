using SharedModels.Enum;
using SharedModels.Models;
using SharedModels.ViewModels;
using System.Collections.Generic;

namespace EmployeeServices.IServices
{
    public interface ITaskServices
    {
        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetAllTasks();
        public ApiResponse<ProjectTaskViewModel> GetTaskById(int id);
        public ApiResponse<ProjectTaskViewModel> CreateTask(ProjectTaskEditViewModel task);
        public ApiResponse<ProjectTaskViewModel> UpdateTask(ProjectTaskEditViewModel taskData, int id);
        public ApiResponse<bool> DeleteTask(int id);
        public ApiResponse<ProjectTaskViewModel> ChangeTaskStatus(int id, TaskStatusEnum status);
        public ApiResponse<ProjectTaskViewModel> ChangeTaskStatus(int taskId, int userId, TaskStatusEnum status);
        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetTasksByUserId(int userId);
        public ApiResponse<ProjectTaskViewModel> GetTaskByIdAndUserId(int taskId, int userId);
        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetAllProjectTasksByUserId(int userId);
        public ApiResponse<ProjectTaskViewModel> CreateTask(int projectId, int employeeId, ProjectTaskEditViewModel taskVm);
    }
}
