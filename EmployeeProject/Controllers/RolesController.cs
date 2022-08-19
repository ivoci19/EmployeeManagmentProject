using EmployeeServices.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.ViewModels;
using System;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var roles = _roleServices.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("GetRole{id:int}")]
        public IActionResult GetRole(int id)
        {
            try
            {
                var role = _roleServices.GetRoleById(id);
                return Ok(role);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateRole")]
        public ActionResult CreateRole(RoleEditViewModel role)
        {
            try
            {
                if (role == null)
                    return BadRequest();

                var createdRole = _roleServices.CreateRole(role);

                return CreatedAtAction(nameof(GetRole), new { id = createdRole.Id }, createdRole);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut("UpdateRole{id:int}")]
        public IActionResult UpdateRole(int id, RoleEditViewModel role)
        {
            try
            {
                var roleToUpdate = _roleServices.GetRoleById(id);

                if (roleToUpdate == null)
                    return NotFound("Role with Id = " + id.ToString() + " not found");

                if (role == null)
                    return BadRequest();

                return Ok(_roleServices.UpdateRole(role, id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteRole{id:int}")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                var role = _roleServices.GetRoleById(id);
                if (role== null)
                    return NotFound("Role with Id = " + id.ToString() + " not found");

                _roleServices.DeleteRole(id);
                return Ok(role);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

       


    }
}
