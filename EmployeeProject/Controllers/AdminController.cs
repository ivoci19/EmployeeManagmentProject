using EmployeesData.Models;
using EmployeesData.Repositories;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enum;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]

    public class AdminController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IProjectServices _projectServices;
        private readonly ITaskServices _taskServices;
        public AdminController( IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
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


        [HttpGet("GetUser{id:int}")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                var user = _userServices.GetUserById(id,true,true);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateUser")]
        public ActionResult CreateUser(UserEditViewModel user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                bool duplicateUsername = _userServices.GetUserByUsername(user.Username);
                if (duplicateUsername)
                {
                    ModelState.AddModelError("username", "Username " + user.Username + " is already in use.");
                    return BadRequest(ModelState);
                }

                var createdUser = _userServices.CreateUser(user);

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("UpdateUser{id:int}")]
        public IActionResult UpdateUser(int id, UserEditViewModel user)
        {
            try
            {
                var userToUpdate = _userServices.GetUserById(id,true,true);

                if (userToUpdate == null)
                    return NotFound("User with Id = " + id.ToString() + " not found");

                if (user == null)
                    return BadRequest();

                return Ok(_userServices.UpdateUser(user,id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteUser{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _userServices.GetUserById(id,false,false);
                if (user.Username == "admin")
                    return BadRequest("You can't delete this user");
                if (user == null)
                    return NotFound("User with Id = " + id.ToString() + " not found");
                _userServices.DeleteUser(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
