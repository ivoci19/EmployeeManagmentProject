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

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetAllUsers", Description = "Get all Users", GroupName = "Users")]
        public IActionResult GetAllUsers()
        {
            var userResponse = _userServices.GetAllUsers();

            if (userResponse.Succeeded)
                return Ok(userResponse);
            return BadRequest(userResponse);
        }


        [HttpGet("GetUser")]
        [ProducesResponseType(typeof(ApiResponse<AllDataUserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<AllDataUserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetUserById", Description = "Get User by Id", GroupName = "Users")]
        public ActionResult<User> GetUser(int userId)
        {
            var userResponse = _userServices.GetUserById(userId);

            if (userResponse.Succeeded)
                return Ok(userResponse);
            return BadRequest(userResponse);
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "CreateUser", Description = "Create new User", GroupName = "Users")]
        public ActionResult CreateUser(UserEditViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var userResponse = _userServices.CreateUser(userVm);

            if (userResponse.Succeeded)
                return Ok(userResponse);
            return BadRequest(userResponse);
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "UpdateUser", Description = "Update new User", GroupName = "Users")]
        public IActionResult UpdateUser([FromQuery] int id, [FromBody] UserEditViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var userResponse = _userServices.UpdateUser(userVm, id);

            if (userResponse.Succeeded)
                return Ok(userResponse);
            return BadRequest(userResponse);
        }

        [HttpDelete("DeleteUser")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "DeleteUser", Description = "Delete a User", GroupName = "Users")]
        public IActionResult DeleteUser(int userId)
        {
            var userResponse = _userServices.DeleteUser(userId);

            if (userResponse.Succeeded)
                return Ok(userResponse);
            return BadRequest(userResponse);
        }
    }
}
