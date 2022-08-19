using AutoMapper;
using EmployeesData.IRepositories;
using EmployeesData.Models;
using EmployeeServices.IServices;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleServices(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public IEnumerable<RoleViewModel> GetAllRoles()
        {
            var role = _roleRepository.Roles;

            return _mapper.Map<IEnumerable<RoleViewModel>>(role); ;
        }


        public RoleViewModel GetRoleById(int id)
        {
            Role role = _roleRepository.GetRoleById(id);
            return _mapper.Map<RoleViewModel>(role);

        }


        public RoleViewModel CreateRole(RoleEditViewModel roleVm)
        {
            Role role = _mapper.Map<Role>(roleVm);
            _roleRepository.SaveRole(role);
            return _mapper.Map<RoleViewModel>(role);
        }

        public bool DeleteRole(int id)
        {
            Role role = _roleRepository.Roles.FirstOrDefault(u => u.Id == id);

            if (role != null)
            {
                return _roleRepository.DeleteRole(id);
            }
            return false;
        }


        public RoleViewModel UpdateRole(RoleEditViewModel roleData, int id)
        {
            Role role = _roleRepository.Roles.FirstOrDefault(e => e.Id == id);
            if (role != null)
            {
                _mapper.Map(roleData, role);
                _roleRepository.SaveRole(role);
                var roleVm = _mapper.Map<RoleViewModel>(role);
                return roleVm;
            }
            return null;
        }

    }
}
