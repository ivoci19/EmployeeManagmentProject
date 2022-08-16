﻿using AutoMapper;
using EmployeesData.IRepositories;
using EmployeesData.Models;
using EmployeeServices.IServices;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XAct.Messages;

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

       
        public UserViewModel CreateUser(UserEditViewModel userVm)
        {
            User user = _mapper.Map<User>(userVm);
            _userRepository.SaveUser(user);
            return _mapper.Map<UserViewModel>(user);
        }

        public bool DeleteUser(int id)
        {
            User user = _userRepository.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                return _userRepository.DeleteUser(id);   
            }
            return false;
        }

        
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var user = _userRepository.Users;

            return _mapper.Map<IEnumerable<UserViewModel>>(user); ;
        }

        
        public bool GetByEmail(string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            if (user != null)
                return true;
            return false;
        }

        
        public UserViewModel GetUserById(int id)
        {
            User user = _userRepository.GetUserById(id);
            return _mapper.Map<UserViewModel>(user);

        }

        public UserViewModel GetUserByUsernameAndPassword(string username, string password)
        {
            User user = _userRepository.GetUserByUsernameAndPassword(username, password);
            return _mapper.Map<UserViewModel>(user);
        }

        //DONE
        public UserViewModel UpdateUser(UserEditViewModel userData, int id)
        {
            User user = _userRepository.Users.FirstOrDefault(e => e.Id == id);
            
            if (user != null)
            {
                _mapper.Map(userData, user);
                _userRepository.SaveUser(user);
                var userVm = _mapper.Map<UserViewModel>(user);
                return userVm;
            }

            return null;
        }
    }
}