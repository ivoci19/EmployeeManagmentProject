using EmployeeProject.Helper;
using EmployeeProject.Helpers;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enum;
using SharedModels.Models;
using SharedModels.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IIdentityHelper _identityHelper;
        public EmployeeController(IUserServices userServices, IIdentityHelper identityHelper)
        {
            _userServices = userServices;
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
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var employee = GetCurrentUser();

            if (employee == null)
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, ErrorMessages.USER_NOT_FOUND));

            var id = employee.Id;
            var userResponse = _userServices.UpdateUser(user, id);

            if (userResponse.Succeeded)
                return Ok(userResponse);
            else
                return BadRequest(userResponse);
        }

        [HttpPut("UpdateProfilePhoto")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "UpdateProfilePhoto", Description = "Update user profile photo", GroupName = "Users")]
        public async Task<IActionResult> UpdateProfilePhoto([FromForm] IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var employee = GetCurrentUser();

                if (employee == null)
                    return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, ErrorMessages.USER_NOT_FOUND));

                if (photo == null)
                    return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.PHOTO_NULL, ErrorMessages.PHOTO_NULL));

                var userId = employee.Id;

                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    if (memoryStream.Length < 5097152)
                    {

                        var photoContent = memoryStream.ToArray();
                        _userServices.UpdateUserPhoto(userId, photoContent);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                        return BadRequest();
                    }
                }
                return Ok();
            }
            return Ok("Invalid");
        }

        private UserViewModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _identityHelper.GetCurrentUser(identity);
            return user;
        }

    }
}
