using EmployeesData.Models;
using EmployeesData.Repositories;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.ViewModels;
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


        [HttpGet("GetUser{id:int}")]
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

        [HttpPost("CreateUser")]
        public ActionResult CreateUser(UserEditViewModel user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                bool duplicateEmail = _userServices.GetByEmail(user.Email);
                if (duplicateEmail)
                {
                    ModelState.AddModelError("email", "Email " + user.Email + " is already in use.");
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
                var userToUpdate = _userServices.GetUserById(id);

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
                var user = _userServices.GetUserById(id);
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
