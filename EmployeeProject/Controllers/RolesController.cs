using EmployeeProject.Helper;
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet("GetAllRoles")]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetAllRoles", Description = "Get all Roles", GroupName = "Roles")]
        public IActionResult GetAllRoles()
        {
            var roleResponse = _roleServices.GetAllRoles();

            if (roleResponse.Succeeded)
                return Ok(roleResponse);
            else
                return BadRequest(roleResponse);
        }


        [HttpGet("GetRole{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetRoleById", Description = "Get Role by Id", GroupName = "Roles")]
        public ActionResult GetUserById(int id)
        {
            var roleResponse = _roleServices.GetRoleById(id);

            if (roleResponse.Succeeded)
                return Ok(roleResponse);
            else
                return BadRequest(roleResponse);
        }

        [HttpPost("CreateRole")]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "CreateRole", Description = "Create new Role", GroupName = "Roles")]
        public ActionResult CreateRole(RoleEditViewModel role)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var roleResponse = _roleServices.CreateRole(role);

            if (roleResponse.Succeeded)
                return Ok(roleResponse);
            else
                return BadRequest(roleResponse);

        }

        [HttpPut("UpdateRole{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<RoleViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "UpdateRole", Description = "Update new Role", GroupName = "Roles")]
        public IActionResult UpdateRole(int id, RoleEditViewModel role)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<UserViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var roleResponse = _roleServices.UpdateRole(role, id);

            if (roleResponse.Succeeded)
                return Ok(roleResponse);
            else
                return BadRequest(roleResponse);
        }

        [HttpDelete("DeleteRole{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "DeleteRole", Description = "Delete a Role", GroupName = "Roles")]
        public IActionResult DeleteRole(int id)
        {
            var roleResponse = _roleServices.DeleteRole(id);

            if (roleResponse.Succeeded)
                return Ok(roleResponse);
            else
                return BadRequest(roleResponse);
        }




    }
}
