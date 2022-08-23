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
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RoleServices(IRoleRepository roleRepository, IMapper mapper, ILogger<RoleServices> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public ApiResponse<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            var role = _roleRepository.Roles;
            IEnumerable<RoleViewModel> roleVm = _mapper.Map<IEnumerable<RoleViewModel>>(role);
            if (role == null)
            {
                return ApiResponse<IEnumerable<RoleViewModel>>.ApiFailResponse(ErrorCodes.ROLE_NOT_FOUND, ErrorMessages.ROLE_NOT_FOUND);
            }

            return ApiResponse<IEnumerable<RoleViewModel>>.ApiOkResponse(roleVm);
        }

        public ApiResponse<RoleViewModel> GetRoleById(int id)
        {
            Role role = _roleRepository.GetRoleById(id);

            if (role == null)
                return ApiResponse<RoleViewModel>.ApiFailResponse(ErrorCodes.ROLE_NOT_FOUND, ErrorMessages.ROLE_NOT_FOUND);

            var roleVm = _mapper.Map<RoleViewModel>(role);
            var response = ApiResponse<RoleViewModel>.ApiOkResponse(roleVm);
            return response;
        }

        public ApiResponse<RoleViewModel> CreateRole(RoleEditViewModel roleVm)
        {
            try
            {
                Role role = _mapper.Map<Role>(roleVm);
                _roleRepository.SaveRole(role);
                var roleViewModel = _mapper.Map<RoleViewModel>(role);
                return ApiResponse<RoleViewModel>.ApiOkResponse(roleViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<RoleViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<bool> DeleteRole(int id)
        {
            try
            {
                Role role = _roleRepository.Roles.FirstOrDefault(i => i.Id == id);

                if (role == null)
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.ROLE_NOT_FOUND, ErrorMessages.ROLE_NOT_FOUND);

                if (role.RoleName == "Administrator")
                    return ApiResponse<bool>.ApiFailResponse(ErrorCodes.INVALID_REQUEST, ErrorMessages.INVALID_REQUEST);

                _roleRepository.DeleteRole(id);
                return ApiResponse<bool>.ApiOkResponse(true);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<bool>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

        public ApiResponse<RoleViewModel> UpdateRole(RoleEditViewModel roleData, int id)
        {
            try
            {
                Role role = _roleRepository.Roles.FirstOrDefault(i => i.Id == id);

                if (role == null)
                    return ApiResponse<RoleViewModel>.ApiFailResponse(ErrorCodes.ROLE_NOT_FOUND, ErrorMessages.ROLE_NOT_FOUND);

                _mapper.Map(roleData, role);
                _roleRepository.SaveRole(role);
                var roleVm = _mapper.Map<RoleViewModel>(role);
                return ApiResponse<RoleViewModel>.ApiOkResponse(roleVm);
            }
            catch (Exception e)
            {
                _logger.LogError(DateTime.Now + " " + e.Message + " " + e.StackTrace);
                return ApiResponse<RoleViewModel>.ApiFailResponse(ErrorCodes.CHANGES_NOT_SAVED, ErrorMessages.SERVER_ERROR);
            }
        }

    }
}
