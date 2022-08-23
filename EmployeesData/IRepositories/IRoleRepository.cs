using EmployeesData.Models;
using System.Linq;

namespace EmployeesData.IRepositories
{
    public interface IRoleRepository
    {
        IQueryable<Role> Roles { get; }
        public void SaveRole(Role role);
        public bool DeleteRole(int roleId);
        public Role GetRoleById(int roleId);
    }
}
