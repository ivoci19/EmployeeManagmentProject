using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public List<Role> Roles
        {
            get
            {
                return _applicationDbContext.Roles
                         .Include(i => i.Users)
                         .Where(i => i.IsActive).ToList();
            }
        }

        public void SaveRole(Role role)
        {
            if (role.Id == 0)
            {
                _applicationDbContext.Roles.Add(role);
            }
            _applicationDbContext.SaveChanges();
        }

        public bool DeleteRole(int id)
        {
            Role role = Roles.Where(i => i.Id == id && i.IsActive).FirstOrDefault();

            if (role != null)
            {
                role.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }


        public Role GetRoleById(int roleId)
        {
            var query = _applicationDbContext.Roles.AsQueryable();

            query = query.Include(i => i.Users).Where(u => u.Id == roleId && u.IsActive);
            var user = query.FirstOrDefault();
            return user;
        }
    }
}
