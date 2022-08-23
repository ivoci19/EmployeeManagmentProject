using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Enum;
using SharedModels.ViewModels;
using System;
using System.Linq;

namespace EmployeesData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IQueryable<User> Users
        {
            get
            {
                return _applicationDbContext.Users
                         .Include(i => i.Role)
                         .Include(i => i.Projects)
                         .Include(i => i.ProjectTasks)
                         .Where(i => i.IsActive);
            }
        }

        public User SaveUser(User user)
        {
            if (user.Id == 0)
            {
                Role role = _applicationDbContext.Roles.Where(i => i.RoleName.ToLower() == "employee").FirstOrDefault();
                user.RoleId = role.Id;
                user.IsActive = true;
                user.Password = Encryptor.MD5Hash(user.Password);
                _applicationDbContext.Users.Add(user);
            }
            _applicationDbContext.SaveChanges();
            return user;
        }

        public void UpdateUserPhoto(int userId, string base64Photo)
        {
            var user = _applicationDbContext.Users.Find(userId);
            user.Photo = base64Photo;
            _applicationDbContext.SaveChanges();
        }

        public bool DeleteUser(int user_id)
        {
            User user = Users.Where(i => i.Id == user_id).FirstOrDefault();

            if (user != null)
            {
                user.IsActive = false;
                user.Username = user.Username + "_" + Guid.NewGuid();
                user.Email = user.Email + "_" + Guid.NewGuid();
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var pass = Encryptor.MD5Hash(password);
            User user = Users.Where(i => i.Username == username && i.Password == pass).FirstOrDefault();
            return user;
        }

        public bool IsUsernameUsed(string username, int id, bool isUpdate)
        {
            //get user list with the username
            var user = Users.Where(i => i.Username == username).ToList();
            //get the user which will be updated
            var userToUpdate = Users.Where(i => i.Username == username && i.Id == id).FirstOrDefault();
            //if we don't have any user in the database with the same username
            //and the isUpdate is false then the username is not used
            if (user.Count == 0 && !isUpdate)
                return false;
            //if isUpdate is true and the count of users which have the same username is greater than 0
            //and the userToUpdate is null then we have duplicated username
            if (isUpdate && user.Count > 0 && userToUpdate == null)
                return true;
            //if isUpdate is true
            if (isUpdate)
                return false;
            return true;
        }

        public bool IsEmailUsed(string email, int id, bool isUpdate)
        {
            //get user list with the email
            var user = Users.Where(i => i.Email == email).ToList();
            //get the user which will be updated
            var userToUpdate = Users.Where(i => i.Email == email && i.Id == id).FirstOrDefault();
            //if we don't have any user in the database with the same email
            //and the isUpdate is false then the email is not used
            if (user.Count == 0 && !isUpdate)
                return false;
            //if isUpdate is true and the count of users which have the same email is greater than 0
            //and the userToUpdate is null then we have duplicated email
            if (isUpdate && user.Count > 0 && userToUpdate == null)
                return true;
            if (isUpdate)
                return false;
            return true;
        }

        public User GetUserByUsername(string username)
        {
            User user = Users.Where(i => i.Username == username).FirstOrDefault();
            return user;
        }

        public User GetUserById(int userId)
        {
            var user = Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }

        public bool HasOpenProjectTasks(int userId)
        {
            var userHasOpenTasks = Users.Where(i => i.Id == userId).FirstOrDefault().ProjectTasks.Any(i => i.TaskStatus != TaskStatusEnum.DONE);
            return userHasOpenTasks;
        }

        public bool HasOpenProjectTasks(int userId, int projectId)
        {
            var userHasOpenTasks = Users.Where(i => i.Id == userId && i.Projects.Any(i => i.Id == projectId))
                                        .FirstOrDefault().ProjectTasks.Any(i => i.TaskStatus != TaskStatusEnum.DONE);
            return userHasOpenTasks;
        }

    }
}
