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
    public class ProjectServices : IProjectServices
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProjectServices(IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper, ILogger<ProjectServices> logger)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public ApiResponse<IEnumerable<ProjectViewModel>> GetAllProjects(UserViewModel user)
        {
            if (user.RoleName.ToLower() == "administrator")
            {
                var project = _projectRepository.Projects;
                IEnumerable<ProjectViewModel> projectVm = _mapper.Map<IEnumerable<ProjectViewModel>>(project);

                return ApiResponse<IEnumerable<ProjectViewModel>>.ApiOkResponse(projectVm);
            }
            else if (user.RoleName.ToLower() == "employee")
            {
                //Get only the projects that the employee is part of
                IEnumerable<Project> projects = _projectRepository.GetProjectsByUserId(user.Id);

                var projectVm = _mapper.Map<IEnumerable<ProjectViewModel>>(projects);

                return ApiResponse<IEnumerable<ProjectViewModel>>.ApiOkResponse(projectVm);
            }
            else
            {
                return ApiResponse<IEnumerable<ProjectViewModel>>.ApiFailResponse(ErrorCodes.UNAUTHORIZED, ErrorMessages.UNAUTHORIZED);
            }

        }

        public ApiResponse<AllDataProjectViewModel> GetProjectById(int projectId, UserViewModel user)
        {
            if (user.RoleName.ToLower() == "administrator")
            {
                Project project = _projectRepository.GetProjectById(projectId);

                if (project == null)
                    return ApiResponse<AllDataProjectViewModel>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);

                var projectVm = _mapper.Map<AllDataProjectViewModel>(project);
                var response = ApiResponse<AllDataProjectViewModel>.ApiOkResponse(projectVm);
                return response;
            }
            else if (user.RoleName.ToLower() == "employee")
            {
                //This method gets the project by project id and the logged in user Id
                var project = _projectRepository.GetProjectByUserId(user.Id, projectId);

                if (project == null)
                    return ApiResponse<AllDataProjectViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_ISNT_IN_PROJECT, ErrorMessages.EMPLOYEE_ISNT_IN_PROJECT);

                var projectVm = _mapper.Map<AllDataProjectViewModel>(project);

                return ApiResponse<AllDataProjectViewModel>.ApiOkResponse(projectVm);
            }
            else
            {
                return ApiResponse<AllDataProjectViewModel>.ApiFailResponse(ErrorCodes.UNAUTHORIZED, ErrorMessages.UNAUTHORIZED);
            }
        }

        public ApiResponse<ProjectViewModel> CreateProject(ProjectEditViewModel projectVm)
        {
            try
            {
                if (_projectRepository.IsCodeUsed(projectVm.Code, 0, false))
                    return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.CODE_ALREADY_USED, ErrorMessages.CODE_ALREADY_USED);

                Project project = _mapper.Map<Project>(projectVm);
                _projectRepository.SaveProject(project);
                var projectViewModel = _mapper.Map<ProjectViewModel>(project);

                return ApiResponse<ProjectViewModel>.ApiOkResponse(projectViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<ProjectViewModel> UpdateProject(ProjectEditViewModel projectData, int id)
        {
            try
            {
                Project project = _projectRepository.Projects.FirstOrDefault(p => p.Id == id);

                if (project == null)
                    return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);

                if (_projectRepository.IsCodeUsed(projectData.Code, id, true))
                    return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.CODE_ALREADY_USED, ErrorMessages.CODE_ALREADY_USED);

                _mapper.Map(projectData, project);
                _projectRepository.SaveProject(project);
                var projectVm = _mapper.Map<ProjectViewModel>(project);

                return ApiResponse<ProjectViewModel>.ApiOkResponse(projectVm);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<bool> DeleteProject(int id)
        {
            try
            {
                Project project = _projectRepository.Projects.FirstOrDefault(p => p.Id == id);

                if (project == null)
                {
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);
                }

                var projectHasOpenTasks = _projectRepository.HasOpenProjectTasks(id);
                if (projectHasOpenTasks)
                {
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.PROJECT_HAS_OPEN_TASKS, ErrorMessages.PROJECT_HAS_OPEN_TASKS);
                }
                _projectRepository.DeleteProject(id);

                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<AllDataProjectViewModel> AddEmployeeToProject(int employeeId, int projectId)
        {
            try
            {
                //Checks for the employee in the database
                User employee = _userRepository.GetUserById(employeeId);
                if (employee == null)
                    return ApiResponse<AllDataProjectViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_NOT_FOUND, ErrorMessages.EMPLOYEE_NOT_FOUND);

                //Checks for the project in the database
                Project project = _projectRepository.GetProjectById(projectId);
                if (project == null)
                    return ApiResponse<AllDataProjectViewModel>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);

                //Checks if the employee is part of the project already
                if (_projectRepository.GetProjectByUserId(employeeId, projectId) != null)
                    return ApiResponse<AllDataProjectViewModel>.ApiFailResponse(ErrorCodes.EMPLOYEE_IS_IN_PROJECT, ErrorMessages.EMPLOYEE_IS_IN_PROJECT);

                Project updatedProject = _projectRepository.AddEmployeeToProject(employeeId, projectId, employee, project);
                var projectVm = _mapper.Map<AllDataProjectViewModel>(updatedProject);

                return ApiResponse<AllDataProjectViewModel>.ApiOkResponse(projectVm);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<AllDataProjectViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<bool> RemoveEmployeeFromProject(int employeeId, int projectId)
        {
            try
            {
                //Checks if the employee exists in the database
                User employee = _userRepository.GetUserById(employeeId);
                if (employee == null)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.EMPLOYEE_NOT_FOUND, ErrorMessages.EMPLOYEE_NOT_FOUND);

                //Checks if the project exists in the database
                Project project = _projectRepository.GetProjectById(projectId);
                if (project == null)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.PROJECT_NOT_FOUND, ErrorMessages.PROJECT_NOT_FOUND);

                //Checks if the employee has open tasks in this project
                if (_userRepository.HasOpenProjectTasks(employeeId, projectId))
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.EMPLOYEE_HAS_OPEN_TASKS, ErrorMessages.EMPLOYEE_HAS_OPEN_TASKS);

                Project updatedProject = _projectRepository.RemoveEmployeeFromProject(employeeId, projectId, employee);
                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

    }
}
