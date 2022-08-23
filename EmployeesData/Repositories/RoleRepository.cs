using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeesData.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IQueryable<Role> Roles
        {
            get
            {
                return _applicationDbContext.Roles
                         .Include(i => i.Users)
                         .Where(i => i.IsActive);
            }
        }

        public void SaveRole(Role role)
        {
            if (role.Id == 0)
            {
                role.IsActive = true;
                _applicationDbContext.Roles.Add(role);
            }
            _applicationDbContext.SaveChanges();
        }

        public bool DeleteRole(int roleId)
        {
            Role role = Roles.Where(i => i.Id == roleId && i.IsActive).FirstOrDefault();
            role.IsActive = false;
            _applicationDbContext.SaveChanges();
            return true;
        }

        public Role GetRoleById(int roleId)
        {
            var rolesQuery = _applicationDbContext.Roles.AsQueryable();

            rolesQuery = rolesQuery.Include(i => i.Users).Where(u => u.Id == roleId && u.IsActive);
            var user = rolesQuery.FirstOrDefault();
            return user;
        }
    }
}
