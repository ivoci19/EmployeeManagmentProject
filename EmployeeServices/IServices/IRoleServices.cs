using SharedModels.Models;
using SharedModels.ViewModels;
using System.Collections.Generic;

namespace EmployeeServices.IServices
{
    public interface IRoleServices
    {
        public ApiResponse<RoleViewModel> GetRoleById(int id);
        public ApiResponse<RoleViewModel> CreateRole(RoleEditViewModel role);
        public ApiResponse<RoleViewModel> UpdateRole(RoleEditViewModel role, int id);
        public ApiResponse<bool> DeleteRole(int id);
        public ApiResponse<IEnumerable<RoleViewModel>> GetAllRoles();
    }
}
