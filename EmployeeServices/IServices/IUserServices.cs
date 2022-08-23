using SharedModels.Models;
using SharedModels.ViewModels;
using System.Collections.Generic;

namespace EmployeeServices.IServices
{
    public interface IUserServices
    {
        public ApiResponse<IEnumerable<UserViewModel>> GetAllUsers();
        public ApiResponse<UserViewModel> GetUserById(int id);
        public ApiResponse<UserViewModel> CreateUser(UserEditViewModel user);
        public ApiResponse<UserViewModel> UpdateUser(UserEditViewModel userData, int id);
        public ApiResponse<bool> DeleteUser(int id);
        public UserViewModel GetUserByUsername(string usernamee);
        public UserViewModel GetUserByUsernameAndPassword(string username, string password);
        ApiResponse<bool> UpdateUserPhoto(int userId, byte[] photoContent);
    }
}
