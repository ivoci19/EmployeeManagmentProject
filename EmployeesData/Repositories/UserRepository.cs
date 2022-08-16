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
            get { return _applicationDbContext.Users.Include(i => i.Role).Where(i => i.IsActive).ToList(); }
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                user.Password = Encryptor.MD5Hash(user.Password);
                user.RoleId = 1;
                user.CreatedBy = 1; 
                user.IsActive = true;
                _applicationDbContext.Users.Add(user);
            }
            user.UpdatedBy = 1;
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

        public User GetUserByEmail(string email)
        {
            User user = Users.Where(i => i.Email == email && i.IsActive).FirstOrDefault();
            return user;

        }

        public User GetUserById(int id)
        {
            User user = Users.Where(i => i.Id == id && i.IsActive).FirstOrDefault();
            return user;
        }

    }
}
