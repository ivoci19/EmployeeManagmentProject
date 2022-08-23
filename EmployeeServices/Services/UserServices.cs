using AutoMapper;
using EmployeesData.IRepositories;
using EmployeesData.Models;
using EmployeeServices.IServices;
using Microsoft.Extensions.Logging;
using SharedModels.Enum;
using SharedModels.Models;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeServices.Services
{

    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserServices(IUserRepository userRepository, IMapper mapper, ILogger<UserServices> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public ApiResponse<IEnumerable<UserViewModel>> GetAllUsers()
        {

            var users = _userRepository.Users;
            IEnumerable<UserViewModel> userVm = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return ApiResponse<IEnumerable<UserViewModel>>.ApiOkResponse(userVm);
        }

        public ApiResponse<UserViewModel> GetUserById(int id)
        {
            User user = _userRepository.GetUserById(id);

            if (user == null)
                return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.USER_NOT_FOUND, ErrorMessages.USER_NOT_FOUND);

            var userVm = _mapper.Map<UserViewModel>(user);
            var response = ApiResponse<UserViewModel>.ApiOkResponse(userVm);
            return response;
        }

        public ApiResponse<UserViewModel> CreateUser(UserEditViewModel userVm)
        {
            try
            {
                if (_userRepository.IsUsernameUsed(userVm.Username, 0, false))
                    return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.USERNAME_ALREADY_USED, ErrorMessages.USERNAME_ALREADY_USED);

                if (_userRepository.IsEmailUsed(userVm.Email, 0, false))
                    return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.EMAIL_ALREADY_USED, ErrorMessages.EMAIL_ALREADY_USED);

                User user = _mapper.Map<User>(userVm);
                User createdUser = _userRepository.SaveUser(user);
                var userViewModel = _mapper.Map<UserViewModel>(createdUser);
                return ApiResponse<UserViewModel>.ApiOkResponse(userViewModel);

            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<bool> DeleteUser(int id)
        {
            try
            {
                User user = _userRepository.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.USER_NOT_FOUND, ErrorMessages.USER_NOT_FOUND);

                if (user.Username == "admin")
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.INVALID_REQUEST, ErrorMessages.INVALID_REQUEST);

                var userHasOpenTasks = _userRepository.HasOpenProjectTasks(id);
                if (userHasOpenTasks)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.USER_HAS_OPENED_TASKS, ErrorMessages.USER_HAS_OPENED_TASKS);

                _userRepository.DeleteUser(id);
                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.BAD_REQUEST, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<UserViewModel> UpdateUser(UserEditViewModel userData, int id)
        {
            try
            {
                User user = _userRepository.Users.FirstOrDefault(i => i.Id == id);

                if (user == null)
                    return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.USER_NOT_FOUND, ErrorMessages.USER_NOT_FOUND);

                if (_userRepository.IsUsernameUsed(userData.Username, id, true))
                    return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.USERNAME_ALREADY_USED, ErrorMessages.USERNAME_ALREADY_USED);

                if (_userRepository.IsEmailUsed(userData.Email, id, true))
                    return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.EMAIL_ALREADY_USED, ErrorMessages.EMAIL_ALREADY_USED);
                _mapper.Map(userData, user);
                if (userData.Password != null)
                    user.Password = Encryptor.MD5Hash(userData.Password);
                var updatedUser = _userRepository.SaveUser(user);
                var userVm = _mapper.Map<UserViewModel>(updatedUser);
                return ApiResponse<UserViewModel>.ApiOkResponse(userVm);

            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, ErrorMessages.SERVER_ERROR);
            }
        }

        public UserViewModel GetUserByUsername(string username)
        {
            User user = _userRepository.GetUserByUsername(username);
            UserViewModel userVm = _mapper.Map<UserViewModel>(user);
            if (user != null)
                return userVm;
            return null;
        }

        public UserViewModel GetUserByUsernameAndPassword(string username, string password)
        {
            User user = _userRepository.GetUserByUsernameAndPassword(username, password);
            return _mapper.Map<UserViewModel>(user);
        }

        public ApiResponse<bool> UpdateUserPhoto(int userId, byte[] photoContent)
        {
            try
            {
                var base64Photo = Convert.ToBase64String(photoContent);
                _userRepository.UpdateUserPhoto(userId, base64Photo);
                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.BAD_REQUEST, ErrorMessages.SERVER_ERROR);
            }

        }
    }
}
