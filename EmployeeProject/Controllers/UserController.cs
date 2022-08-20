using EmployeeProject.Helper;
using EmployeesData.Models;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enum;
using SharedModels.Models;
using SharedModels.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]

    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IProjectServices _projectServices;
        private readonly ITaskServices _taskServices;
        public UserController(IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetAllProjects", Description = "Get all Users", GroupName = "Users")]
        public IActionResult GetAllUsers()
        {
            var userResponse = _userServices.GetAllUsers();
            if (userResponse.Succeeded)
                return Ok(userResponse);
            else
                return BadRequest(userResponse);
        }


        [HttpGet("GetUser{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetUserById", Description = "Get User by Id", GroupName = "Users")]
        public ActionResult<User> GetUserById(int id)
        {
            var userResponse = _userServices.GetUserById(id, true, true);
            if (userResponse.Succeeded)
                return Ok(userResponse);
            else
                return BadRequest(userResponse);
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "CreateUser", Description = "Create new User", GroupName = "Users")]
        public ActionResult CreateUser(UserEditViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var userResponse = _userServices.CreateUser(user);

            if (userResponse.Succeeded)
                return Ok(userResponse);
            else
                return BadRequest(userResponse);

        }

        [HttpPut("UpdateUser{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "UpdateUser", Description = "Update new User", GroupName = "Users")]
        public IActionResult UpdateUser(int id, UserEditViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }
            var userResponse = _userServices.UpdateUser(user, id);
            if (userResponse.Succeeded)
                return Ok(userResponse);
            else
                return BadRequest(userResponse);
        }

        [HttpDelete("DeleteUser{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "DeleteUser", Description = "Delete a User", GroupName = "Users")]
        public IActionResult DeleteUser(int id)
        {
            var userResponse = _userServices.DeleteUser(id);
            if (userResponse.Succeeded)
                return Ok(userResponse);
            else
                return BadRequest(userResponse);
        }
    }
}
