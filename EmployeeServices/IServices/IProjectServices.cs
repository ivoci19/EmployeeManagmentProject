using SharedModels.Models;
using SharedModels.ViewModels;
using System.Collections.Generic;

namespace EmployeeServices.IServices
{
    public interface IProjectServices
    {
        public ApiResponse<IEnumerable<ProjectViewModel>> GetAllProjects();
        public ApiResponse<ProjectViewModel> GetProjectById(int id);
        public ApiResponse<ProjectViewModel> CreateProject(ProjectEditViewModel projectVm);
        public ApiResponse<ProjectViewModel> UpdateProject(ProjectEditViewModel projectData, int id);
        public ApiResponse<bool> DeleteProject(int id);
        public ApiResponse<IEnumerable<ProjectViewModel>> GetEmployeeProjects(int employeeId);
        public ApiResponse<ProjectViewModel> AddEmployeeToProject(int employeeId, int projectId);
        public ApiResponse<bool> RemoveEmployeeFromProject(int employeeId, int projectId);
        public ApiResponse<ProjectViewModel> GetEmployeeProject(int userId, int projectId);

    }
}
