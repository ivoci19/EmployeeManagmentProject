using EmployeeProject.Helpers;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enum;
using SharedModels.Models;
using SharedModels.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IProjectServices _projectServices;
        private readonly ITaskServices _taskServices;
        private readonly IIdentityHelper _identityHelper;
        public EmployeeController(IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices, IIdentityHelper identityHelper)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
            _identityHelper = identityHelper;
        }

        [HttpGet("GetProfileData")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetProfileData", Description = "Get logged in employee data", GroupName = "Employee")]
        public IActionResult GetProfileData()
        {
            var user = GetCurrentUser();
            if (user != null)
                return Ok(ApiResponse<UserViewModel>.ApiOkResponse(user));
            else
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, ErrorMessages.USER_NOT_FOUND));
        }

        [HttpPut("UpdateProfileData")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "UpdateProfileData", Description = "Update profile data", GroupName = "Employee")]
        public IActionResult UpdateProfileData(UserEditViewModel user)
        {
            if (user == null)
                return BadRequest();

            var employee = GetCurrentUser();

            if (employee == null)
                return BadRequest();

            var id = employee.Id;
            var userResponse = _userServices.UpdateUser(user, id);

            if (userResponse.Succeeded)
                return Ok(userResponse);
            else
                return BadRequest(userResponse);
        }

        private UserViewModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _identityHelper.GetCurrentUser(identity);
            return user;
        }

    }
}
