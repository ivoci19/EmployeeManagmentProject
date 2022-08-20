using SharedModels.Models;
using SharedModels.ViewModels;
using System.Collections.Generic;

namespace EmployeeServices.IServices
{
    public interface IUserServices
    {
        public ApiResponse<IEnumerable<UserViewModel>> GetAllUsers();
        public ApiResponse<UserViewModel> GetUserById(int id, bool includeProjects, bool includeTasks);
        public ApiResponse<UserViewModel> CreateUser(UserEditViewModel user);
        public ApiResponse<UserViewModel> UpdateUser(UserEditViewModel user, int id);
        public ApiResponse<bool> DeleteUser(int id);
        public UserViewModel GetUserByUsername(string usernamee);
        public UserViewModel GetLoggedInUser(string username);
        public UserViewModel GetUserByUsernameAndPassword(string username, string password);

    }
}
