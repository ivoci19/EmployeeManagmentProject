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
    public class TaskServices : ITaskServices
    {

        private readonly IProjectTaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TaskServices(IProjectTaskRepository taskRepository, IMapper mapper, IProjectRepository projectRepository,IUserRepository userRepository, ILogger<TaskServices> logger)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetAllTasks(UserViewModel user)
        {
            if (user.RoleName.ToLower() == "administrator")
            {
                var task = _taskRepository.ProjectTasks;
                IEnumerable<ProjectTaskViewModel> taskVm = _mapper.Map<IEnumerable<ProjectTaskViewModel>>(task);
                if (task == null)
                {
                    return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);
                }

                return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiOkResponse(taskVm);
            }
            else if (user.RoleName.ToLower() == "employee")
            {
                IEnumerable<Project> projects = _projectRepository.GetProjectsByUserId(user.Id);
                if (projects == null)
                    return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.PROJECTS_NOT_FOUND, ErrorMessages.PROJECTS_NOT_FOUND);

                IEnumerable<ProjectTask> tasks = _taskRepository.GetTasksOfUserProjects(projects);

                if (tasks == null)
                    return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.TASKS_NOT_FOUND, ErrorMessages.TASKS_NOT_FOUND);

                var tasksVm = _mapper.Map<IEnumerable<ProjectTaskViewModel>>(tasks);
                return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiOkResponse(tasksVm);
            }
            else
            {
                return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.UNAUTHORIZED, ErrorMessages.UNAUTHORIZED);
            }
        }

        public ApiResponse<ProjectTaskViewModel> GetTaskById(int taskId, UserViewModel user)
        {
            if (user.RoleName.ToLower() == "administrator")
            {
                ProjectTask task = _taskRepository.GetTaskById(taskId);

                if (task == null)
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

                var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                var response = ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
                return response;
            }
            else if (user.RoleName.ToLower() == "employee")
            {
                var task = _taskRepository.GetTaskByIdAndUserId(taskId, user.Id);

                if (task == null)
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

                var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
            }
            else
            {
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.UNAUTHORIZED, ErrorMessages.UNAUTHORIZED);
            }
        }

        public ApiResponse<ProjectTaskViewModel> CreateTask(ProjectTaskEditViewModel taskVm, UserViewModel user)
        {
            try
            {
                if (user.RoleName.ToLower() == "administrator")
                {
                    if(taskVm.AssignedTo != null)
                    {
                        var id = taskVm.AssignedTo ?? default(int);
                        if (_userRepository.GetUserById(id)==null)
                            return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_NOT_FOUND, ErrorMessages.EMPLOYEE_NOT_FOUND);
                    }
                    if(_projectRepository.GetProjectById(taskVm.ProjectId) == null)
                    {
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);
                    }
                    ProjectTask task = _mapper.Map<ProjectTask>(taskVm);
                    _taskRepository.SaveTask(task);
                    var taskViewModel = _mapper.Map<ProjectTaskViewModel>(task);
                    return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskViewModel);
                }
                if (user.RoleName.ToLower() == "employee")
                {
                    Project project = _projectRepository.GetProjectByUserId(user.Id, taskVm.ProjectId);
                    if (project == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_ISNT_IN_PROJECT, ErrorMessages.EMPLOYEE_ISNT_IN_PROJECT);

                    ProjectTask task = _mapper.Map<ProjectTask>(taskVm);
                    _taskRepository.SaveTask(task);
                    var taskViewModel = _mapper.Map<ProjectTaskViewModel>(task);

                    return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskViewModel);
                }
                else
                {
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.UNAUTHORIZED, ErrorMessages.UNAUTHORIZED);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<bool> DeleteTask(int id)
        {
            try
            {
                ProjectTask task = _taskRepository.ProjectTasks.FirstOrDefault(u => u.Id == id);

                if (task == null)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);
                if (task.TaskStatus == TaskStatusEnum.PENDING || task.TaskStatus == TaskStatusEnum.IN_PROGRESS)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.TASK_IS_OPEN, ErrorMessages.TASK_IS_OPEN);

                _taskRepository.DeleteTask(id);
                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<ProjectTaskViewModel> UpdateTask(ProjectTaskEditViewModel taskData, int id)
        {
            try
            {
                ProjectTask task = _taskRepository.ProjectTasks.FirstOrDefault(i => i.Id == id);

                if (task == null)
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

                _mapper.Map(taskData, task);
                _taskRepository.SaveTask(task);
                var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
            }
            catch
            {
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<ProjectTaskViewModel> ChangeTaskStatus(int taskId, TaskStatusEnum status, UserViewModel user)
        {
            try
            {
                if (user.RoleName.ToLower() == "administrator")
                {
                    ProjectTask task = _taskRepository.ProjectTasks.FirstOrDefault(p => p.Id == taskId);

                    if (task == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

                    if (task.TaskStatus == TaskStatusEnum.DONE)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_IS_DONE, ErrorMessages.TASK_IS_DONE);

                    task.TaskStatus = status;
                    _mapper.Map<ProjectTaskViewModel>(task);
                    _taskRepository.SaveTask(task);
                    var taskVm = _mapper.Map<ProjectTaskViewModel>(task);

                    return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
                }
                else if (user.RoleName.ToLower() == "employee")
                {
                    ProjectTask task = _taskRepository.GetTaskByIdAndUserId(taskId, user.Id);

                    if (task == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.USER_RESTRICTED, ErrorMessages.USER_RESTRICTED);

                    if (task.TaskStatus == TaskStatusEnum.DONE)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_IS_DONE, ErrorMessages.TASK_IS_DONE);

                    task.TaskStatus = status;
                    _taskRepository.SaveTask(task);
                    var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                    return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
                }
                else
                {
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.UNAUTHORIZED, ErrorMessages.UNAUTHORIZED);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<ProjectTaskViewModel> AssignTaskToEmployee(int taskId, int employeeId, UserViewModel userVm)
        {
            try
            {
                if (userVm.RoleName.ToLower() == "administrator")
                {
                    ProjectTask task = _taskRepository.GetTaskById(taskId);

                    if (task == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

                    if (task.AssignedTo != null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_IS_ASSIGNED_TO_ANOTHER_EMPLOYEE, ErrorMessages.TASK_IS_ASSIGNED_TO_ANOTHER_EMPLOYEE);

                    if (task.AssignedTo == employeeId)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_IS_ALREADY_ASSIGNED, ErrorMessages.TASK_IS_ALREADY_ASSIGNED);

                    Project project = _projectRepository.GetProjectByUserId(employeeId, task.ProjectId);
                    if (project == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_NOT_PART_OF_PROJECT, ErrorMessages.EMPLOYEE_NOT_PART_OF_PROJECT);

                    task.AssignedTo = employeeId;
                    _taskRepository.SaveTask(task);
                    var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                    return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);

                }
                else if (userVm.RoleName.ToLower() == "employee")
                {

                    ProjectTask task = _taskRepository.GetTaskById(taskId);

                    if (task == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

                    if (task.CreatedBy != userVm.Username)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_CREATED_BY_ANOTHER_USER, ErrorMessages.TASK_CREATED_BY_ANOTHER_USER);

                    if (task.AssignedTo != null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_IS_ASSIGNED_TO_ANOTHER_EMPLOYEE, ErrorMessages.TASK_IS_ASSIGNED_TO_ANOTHER_EMPLOYEE);

                    if (task.AssignedTo == employeeId)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_IS_ALREADY_ASSIGNED, ErrorMessages.TASK_IS_ALREADY_ASSIGNED);

                    Project projectEmployee = _projectRepository.GetProjectByUserId(userVm.Id, task.ProjectId);
                    if (projectEmployee == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_ISNT_IN_PROJECT, ErrorMessages.EMPLOYEE_ISNT_IN_PROJECT);

                    Project project = _projectRepository.GetProjectByUserId(employeeId, task.ProjectId);
                    if (project == null)
                        return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_NOT_PART_OF_PROJECT, ErrorMessages.EMPLOYEE_NOT_PART_OF_PROJECT);

                    task.AssignedTo = employeeId;
                    _taskRepository.SaveTask(task);
                    var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                    return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
                }
                else
                {
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.UNAUTHORIZED, ErrorMessages.UNAUTHORIZED);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);

            }
        }
    }
}
