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
    public class ProjectServices : IProjectServices
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectServices(IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper, IProjectTaskRepository projectTaskRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public ApiResponse<IEnumerable<ProjectViewModel>> GetAllProjects()
        {

            var project = _projectRepository.Projects;
            IEnumerable<ProjectViewModel> projectVm = _mapper.Map<IEnumerable<ProjectViewModel>>(project);

            if (project == null)
            {
                return ApiResponse<IEnumerable<ProjectViewModel>>.ApiFailResponse(ErrorCodes.RECORD_NOT_FOUND, ErrorMessages.RECORD_NOT_FOUND);
            }

            return ApiResponse<IEnumerable<ProjectViewModel>>.ApiOkResponse(projectVm);
        }

        public ApiResponse<ProjectViewModel> GetProjectById(int id)
        {
            Project project = _projectRepository.GetProjectById(id);

            if (project == null)
                return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.RECORD_NOT_FOUND, ErrorMessages.RECORD_NOT_FOUND);

            var projectVm = _mapper.Map<ProjectViewModel>(project);
            var response = ApiResponse<ProjectViewModel>.ApiOkResponse(projectVm);
            return response;
        }

        public ApiResponse<ProjectViewModel> CreateProject(ProjectEditViewModel projectVm)
        {
            try
            {
                Project project = _mapper.Map<Project>(projectVm);
                _projectRepository.SaveProject(project);
                var projectViewModel = _mapper.Map<ProjectViewModel>(project);

                return ApiResponse<ProjectViewModel>.ApiOkResponse(projectViewModel);
            }
            catch
            {
                return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<ProjectViewModel> UpdateProject(ProjectEditViewModel projectData, int id)
        {
            try
            {
                Project project = _projectRepository.Projects.FirstOrDefault(p => p.Id == id);

                if (project == null)
                    return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.RECORD_NOT_FOUND, ErrorMessages.RECORD_NOT_FOUND);

                _mapper.Map(projectData, project);
                _projectRepository.SaveProject(project);
                var projectVm = _mapper.Map<ProjectViewModel>(project);

                return ApiResponse<ProjectViewModel>.ApiOkResponse(projectVm);
            }
            catch
            {
                return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<bool> DeleteProject(int id)
        {
            try
            {
                Project project = _projectRepository.Projects.FirstOrDefault(p => p.Id == id);

                if (project == null)
                {
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.RECORD_NOT_FOUND, ErrorMessages.RECORD_NOT_FOUND);
                }

                IEnumerable<ProjectTask> projectTasks = _projectTaskRepository.GetTasksByProjectId(id);

                foreach (ProjectTask task in projectTasks)
                {
                    if (_projectTaskRepository.IsTaskStatusDone(task.Id))
                        return ApiResponse<bool>.ApiFailResponse(ErrorCodes.TASK_IS_OPEN, ErrorMessages.TASK_IS_OPEN);
                }

                _projectRepository.DeleteProject(id);

                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<ProjectViewModel> AddEmployeeToProject(int employeeId, int projectId)
        {
            try
            {
                User employee = _userRepository.GetUserById(employeeId, true, true);
                if (employee == null)
                    return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.USER_NOT_FOUND, ErrorMessages.USER_NOT_FOUND);

                Project project = _projectRepository.GetProjectById(projectId);
                if (project == null)
                    return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);

                if (_projectRepository.GetProjectByUserId(employeeId, projectId) == null)
                    return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_ISNT_IN_PROJECT, ErrorMessages.EMPLOYEE_ISNT_IN_PROJECT);

                Project updatedProject = _projectRepository.AddEmployeeToProject(employeeId, projectId, employee, project);
                ProjectViewModel projectVm = _mapper.Map<ProjectViewModel>(updatedProject);

                return ApiResponse<ProjectViewModel>.ApiOkResponse(projectVm);
            }
            catch (Exception e)
            {
                return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<bool> RemoveEmployeeFromProject(int employeeId, int projectId)
        {
            try
            {
                User employee = _userRepository.GetUserById(employeeId, true, true);
                if (employee == null)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.USER_NOT_FOUND, ErrorMessages.USER_NOT_FOUND);
               
                Project project = _projectRepository.GetProjectById(projectId);
                if (project == null)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);

                IEnumerable<ProjectTask> tasks = _projectTaskRepository.GetTasksByUserId(employeeId);

                foreach (var task in tasks)
                {
                    if (task.TaskStatus == TaskStatusEnum.PENDING || task.TaskStatus == TaskStatusEnum.IN_PROGRESS)
                        return ApiResponse<bool>.ApiFailResponse(ErrorCodes.USER_HAS_OPENED_TASKS, ErrorMessages.USER_HAS_OPENED_TASKS);
                }

                Project updatedProject = _projectRepository.RemoveEmployeeFromProject(employeeId, projectId, employee);
                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.CHANGES_NOT_SAVED);
            }
        }

        public ApiResponse<IEnumerable<ProjectViewModel>> GetEmployeeProjects(int userId)
        {
            IEnumerable<Project> projects = _projectRepository.GetProjectsByUserId(userId);

            if (projects == null)
                return ApiResponse<IEnumerable<ProjectViewModel>>.ApiFailResponse(ErrorCodes.PROJECTS_NOT_FOUND, ErrorMessages.PROJECTS_NOT_FOUND);

            var projectVm = _mapper.Map<IEnumerable<ProjectViewModel>>(projects);

            return ApiResponse<IEnumerable<ProjectViewModel>>.ApiOkResponse(projectVm);
        }


        public ApiResponse<ProjectViewModel> GetEmployeeProject(int userId, int projectId)
        {
            IEnumerable<Project> projects = _projectRepository.GetProjectsByUserId(userId);

            Project project = projects.Where(p => p.Id == projectId).FirstOrDefault();

            if (project == null)
                return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_ISNT_IN_PROJECT, ErrorMessages.EMPLOYEE_ISNT_IN_PROJECT);
            
            var projectVm = _mapper.Map<ProjectViewModel>(project);

            return ApiResponse<ProjectViewModel>.ApiOkResponse(projectVm);
        }

    }
}
