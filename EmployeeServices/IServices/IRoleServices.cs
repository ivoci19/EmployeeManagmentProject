using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.IServices
{
    public interface IRoleServices
    {
        public RoleViewModel GetRoleById(int id);
        public RoleViewModel CreateRole(RoleEditViewModel role);
        public RoleViewModel UpdateRole(RoleEditViewModel role, int id);
        public bool DeleteRole(int id);
        public IEnumerable<RoleViewModel> GetAllRoles();
    }
}
