using EmployeesData.Models;
using System.Collections.Generic;

namespace EmployeesData.IRepositories
{
    public interface IRoleRepository
    {
        List<Role> Roles { get; }
        public void SaveRole(Role role);
        public bool DeleteRole(int id);
        public Role GetRoleById(int id);
    }
}
