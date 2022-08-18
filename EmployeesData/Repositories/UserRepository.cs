using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public List<User> Users
        {
            get { return _applicationDbContext.Users
                         .Include(i => i.Role)
                         .Include(i => i.Projects)
                         .Include(i => i.ProjectTasks)
                         .Where(i => i.IsActive).ToList(); }
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                user.RoleId = 1;
                user.IsActive = true;
                _applicationDbContext.Users.Add(user);
            }
            _applicationDbContext.SaveChanges();
        }

        public bool DeleteUser(int id)
        {
            User user = Users.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
            
            if (user != null)
            {
                user.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var pass = Encryptor.MD5Hash(password);
            User user = Users.Where(i => i.Username == username && i.IsActive && i.Password == pass).FirstOrDefault();
            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = Users.Where(i => i.Username == username && i.IsActive).FirstOrDefault();
            return user;

        }

        public User GetUserById(int userId, bool includeProjects)
        {
            var query = _applicationDbContext.Users.AsQueryable();
            if (includeProjects)
                query = query.Include(i => i.Projects);

            query = query.Where(u => u.Id == userId && u.IsActive);
            var user = query.FirstOrDefault();
            return user;
        }

    }
}
