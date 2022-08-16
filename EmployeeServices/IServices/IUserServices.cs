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
        public UserViewModel CreateUser(UserEditViewModel user);
        public UserViewModel UpdateUser(UserEditViewModel user, int id);
        public bool DeleteUser(int id);
        public IEnumerable<UserViewModel> GetAllUsers();
        public bool GetByEmail(string email);


    }
}
