using EmployeesData.Models;
using EmployeesData.Repositories;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public AdminController( IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userServices.GetAllUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userServices.GetUserById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
