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
    public class TaskServices : ITaskServices
    {

        private readonly IProjectTaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TaskServices(IProjectTaskRepository taskRepository, IMapper mapper, IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetAllTasks()
        {

            var task = _taskRepository.ProjectTasks;
            IEnumerable<ProjectTaskViewModel> taskVm = _mapper.Map<IEnumerable<ProjectTaskViewModel>>(task);
            if (task == null)
            {
                return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);
            }

            return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiOkResponse(taskVm);
        }

        public ApiResponse<ProjectTaskViewModel> GetTaskById(int id)
        {
            ProjectTask task = _taskRepository.GetTaskById(id);

            if (task == null)
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

            var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
            var response = ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
            return response;
        }

        public ApiResponse<ProjectTaskViewModel> CreateTask(ProjectTaskEditViewModel taskVm)
        {
            try
            {
                ProjectTask task = _mapper.Map<ProjectTask>(taskVm);
                _taskRepository.SaveTask(task);
                var taskViewModel = _mapper.Map<ProjectTaskViewModel>(task);
                return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskViewModel);
            }
            catch (Exception e)
            {
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

        public ApiResponse<ProjectTaskViewModel> ChangeTaskStatus(int id, TaskStatusEnum status)
        {
            try
            {
                ProjectTask task = _taskRepository.ProjectTasks.FirstOrDefault(p => p.Id == id);

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
            catch (Exception e)
            {
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<ProjectTaskViewModel> ChangeTaskStatus(int taskId, int userId, TaskStatusEnum status)
        {
            try
            {
                ProjectTask task = _taskRepository.GetTaskByIdAndUserId(taskId, userId);

                if (task == null)
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.USER_RESTRICTED, ErrorMessages.USER_RESTRICTED);

                if (task.TaskStatus == TaskStatusEnum.DONE)
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_IS_DONE, ErrorMessages.TASK_IS_DONE);

                task.TaskStatus = status;
                _taskRepository.SaveTask(task);
                var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
            }
            catch
            {
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetTasksByUserId(int userId)
        {
            IEnumerable<ProjectTask> tasks = _taskRepository.GetTasksByUserId(userId);

            if (tasks == null)
                return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.TASKS_NOT_FOUND, ErrorMessages.TASKS_NOT_FOUND);

            var tasksVm = _mapper.Map<IEnumerable<ProjectTaskViewModel>>(tasks);
            return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiOkResponse(tasksVm);
        }

        public ApiResponse<IEnumerable<ProjectTaskViewModel>> GetAllProjectTasksByUserId(int userId)
        {
            IEnumerable<Project> projects = _projectRepository.GetProjectsByUserId(userId);
            if (projects == null)
                return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.PROJECTS_NOT_FOUND, ErrorMessages.PROJECTS_NOT_FOUND);

            IEnumerable<ProjectTask> tasks = _taskRepository.GetTasksOfUserProjects(projects);

            if (tasks == null)
                return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiFailResponse(ErrorCodes.TASKS_NOT_FOUND, ErrorMessages.TASKS_NOT_FOUND);

            var tasksVm = _mapper.Map<IEnumerable<ProjectTaskViewModel>>(tasks);
            return ApiResponse<IEnumerable<ProjectTaskViewModel>>.ApiOkResponse(tasksVm);
        }

        public ApiResponse<ProjectTaskViewModel> GetTaskByIdAndUserId(int taskId, int userId)
        {
            var task = _taskRepository.GetTaskByIdAndUserId(taskId, userId);

            if (task == null)
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.TASK_NOT_FOUND, ErrorMessages.TASK_NOT_FOUND);

            var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
            return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskVm);
        }
        public ApiResponse<ProjectTaskViewModel> CreateTask(int projectId, int employeeId, ProjectTaskEditViewModel taskVm)
        {
            try
            {
                User employee = _userRepository.GetUserById(employeeId, true, true);
                if (employee == null)
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.USER_NOT_FOUND, ErrorMessages.USER_NOT_FOUND);

                Project project = _projectRepository.GetProjectByUserId(employeeId, projectId);
                if (project == null)
                    return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_ISNT_IN_PROJECT, ErrorMessages.EMPLOYEE_ISNT_IN_PROJECT);

                ProjectTask task = _mapper.Map<ProjectTask>(taskVm);
                _taskRepository.SaveTask(task);
                var taskViewModel = _mapper.Map<ProjectTaskViewModel>(task);

                return ApiResponse<ProjectTaskViewModel>.ApiOkResponse(taskViewModel);
            }
            catch (Exception e)
            {
                return ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);

            }
        }
    }
}
