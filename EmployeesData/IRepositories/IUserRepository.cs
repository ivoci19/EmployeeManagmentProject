using EmployeesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.IRepositories
{
    public interface IUserRepository
    {
        List<User> Users { get; }
        public void SaveUser(User user);
        public User GetUserByUsernameAndPassword(string username, string password);
        public User GetUserByUsername(string username);
        public User GetUserById(int id, bool includeProject);
        public bool DeleteUser(int UserId);
   

    }
}
