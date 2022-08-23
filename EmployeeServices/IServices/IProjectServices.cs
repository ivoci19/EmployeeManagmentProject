using SharedModels.Models;
using SharedModels.ViewModels;
using System.Collections.Generic;

namespace EmployeeServices.IServices
{
    public interface IProjectServices
    {
        public ApiResponse<IEnumerable<ProjectViewModel>> GetAllProjects(UserViewModel user);
        public ApiResponse<AllDataProjectViewModel> GetProjectById(int projectId, UserViewModel user);
        public ApiResponse<ProjectViewModel> CreateProject(ProjectEditViewModel projectVm);
        public ApiResponse<ProjectViewModel> UpdateProject(ProjectEditViewModel projectData, int id);
        public ApiResponse<bool> DeleteProject(int id);
        public ApiResponse<ProjectViewModel> AddEmployeeToProject(int employeeId, int projectId);
        public ApiResponse<bool> RemoveEmployeeFromProject(int employeeId, int projectId);

    }
}
