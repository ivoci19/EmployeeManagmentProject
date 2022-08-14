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

    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserViewModel CreateUser(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var user = _userRepository.Users;

            return _mapper.Map<IEnumerable<UserViewModel>>(user); ;
        }

        public UserViewModel GetUserById(int id)
        {
            User user = _userRepository.Users.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<UserViewModel>(user);

        }

        public UserViewModel GetUserByUsernameAndPassword(string username, string password)
        {
            var pass = Encryptor.MD5Hash(password);
            User user = _userRepository.Users.FirstOrDefault(o => o.Username == username && o.Password == pass);

            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel UpdateUser(UserViewModel user)
        {
            throw new NotImplementedException();
        }
    }
}
