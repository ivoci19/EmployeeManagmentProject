using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.IServices
{
    public interface IUserServices
    {
        public UserViewModel GetUserByUsernameAndPassword(string username, string password);
        public UserViewModel GetUserById(int id);
        public UserViewModel CreateUser(UserViewModel user);
        public UserViewModel UpdateUser(UserViewModel user);
        public bool DeleteUser(UserViewModel user);
        public IEnumerable<UserViewModel> GetAllUsers();


    }
}
