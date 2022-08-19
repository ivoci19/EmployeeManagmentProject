using EmployeesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
