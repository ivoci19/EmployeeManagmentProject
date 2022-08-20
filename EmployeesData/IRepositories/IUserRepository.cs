using EmployeesData.Models;
using System.Collections.Generic;

namespace EmployeesData.IRepositories
{
    public interface IUserRepository
    {
        List<User> Users { get; }
        public void SaveUser(User user);
        public User GetUserByUsernameAndPassword(string username, string password);
        public User GetUserByUsername(string username);
        public User GetUserById(int id, bool includeProject, bool includeTask);
        public bool DeleteUser(int UserId);


    }
}
