using AutoMapper;
using EmployeesData.IRepositories;
using EmployeesData.Models;
using EmployeeServices.IServices;
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
        private readonly IProjectTaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository userRepository, IProjectTaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public ApiResponse<IEnumerable<UserViewModel>> GetAllUsers()
        {

            var users = _userRepository.Users;
            IEnumerable<UserViewModel> userVm = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return ApiResponse<IEnumerable<UserViewModel>>.ApiOkResponse(userVm);
        }

        public ApiResponse<UserViewModel> GetUserById(int id, bool includeProjects, bool includeTasks)
        {
            User user = _userRepository.GetUserById(id, includeProjects, includeTasks);

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
                User user = _mapper.Map<User>(userVm);
                _userRepository.SaveUser(user);
                var userViewModel = _mapper.Map<UserViewModel>(user);
                return ApiResponse<UserViewModel>.ApiOkResponse(userViewModel);
            }
            catch (Exception e)
            {
                return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
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

                IEnumerable<ProjectTask> tasks = _taskRepository.GetTasksByUserId(id);

                foreach (var task in tasks)
                {
                    if (task.TaskStatus == TaskStatusEnum.PENDING || task.TaskStatus == TaskStatusEnum.IN_PROGRESS)
                        return ApiResponse<bool>.ApiFailResponse(ErrorCodes.USER_HAS_OPENED_TASKS, ErrorMessages.USER_HAS_OPENED_TASKS);
                }

                _userRepository.DeleteUser(id);
                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<UserViewModel> UpdateUser(UserEditViewModel userData, int id)
        {
            try
            {
                User user = _userRepository.Users.FirstOrDefault(i => i.Id == id);

                if (user == null)
                    return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.USER_NOT_FOUND, ErrorMessages.USER_NOT_FOUND);

                _mapper.Map(userData, user);
                _userRepository.SaveUser(user);
                var userVm = _mapper.Map<UserViewModel>(user);
                return ApiResponse<UserViewModel>.ApiOkResponse(userVm);

            }
            catch
            {
                return ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
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

        public UserViewModel GetLoggedInUser(string username)
        {
            User user = _userRepository.GetUserByUsername(username);
            return _mapper.Map<UserViewModel>(user);
        }



    }
}
