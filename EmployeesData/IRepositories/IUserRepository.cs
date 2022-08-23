using EmployeesData.Models;
using System.Linq;

namespace EmployeesData.IRepositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        public User SaveUser(User user);
        public User GetUserByUsernameAndPassword(string username, string password);
        public bool IsUsernameUsed(string username, int id, bool isUpdate);
        public bool IsEmailUsed(string email, int id, bool isUpdate);
        public User GetUserById(int userId);
        public bool DeleteUser(int userId);
        public User GetUserByUsername(string username);
        public bool HasOpenProjectTasks(int userId);
        public bool HasOpenProjectTasks(int userId, int projectId);
        public void UpdateUserPhoto(int userId, string base64Photo);
    }
}
