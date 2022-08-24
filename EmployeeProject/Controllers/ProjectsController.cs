using EmployeeProject.Helper;
using EmployeeProject.Helpers;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enum;
using SharedModels.Models;
using SharedModels.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectServices _projectServices;
        private readonly IIdentityHelper _identityHelper;
        public ProjectsController(IProjectServices projectServices, IIdentityHelper identityHelper)
        {
            _projectServices = projectServices;
            _identityHelper = identityHelper;
        }

        [HttpGet("GetAllProjects")]
        [ProducesResponseType(typeof(ApiResponse<ProjectViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetAllProjects", Description = "Get all projects", GroupName = "Projects")]
        public IActionResult GetAllProjects()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var projectResponse = _projectServices.GetAllProjects(loggedInUser);

            if (projectResponse.Succeeded)
                return Ok(projectResponse);
            return BadRequest(projectResponse);

        }


        [HttpGet("GetProjectById")]
        [ProducesResponseType(typeof(ApiResponse<AllDataProjectViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<AllDataProjectViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetProjectById", Description = "Get project by id", GroupName = "Projects")]
        public IActionResult GetProjectById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var projectResponse = _projectServices.GetProjectById(id, loggedInUser);

            if (projectResponse.Succeeded)
                return Ok(projectResponse);
            return BadRequest(projectResponse);
        }

        [HttpPost("CreateProject")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(ApiResponse<ProjectViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "CreateProject", Description = "Create new Project", GroupName = "Projects")]
        public ActionResult CreateProject(ProjectEditViewModel project)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var projectResponse = _projectServices.CreateProject(project);

            if (projectResponse.Succeeded)
                return Ok(projectResponse);
            return BadRequest(projectResponse);

        }


        [HttpPut("UpdateProject")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(ApiResponse<ProjectViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "UpdateProject", Description = "Update new Project", GroupName = "Projects")]
        public IActionResult UpdateProject(int id, ProjectEditViewModel project)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<ProjectViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }
            var projectResponse = _projectServices.UpdateProject(project, id);

            if (projectResponse.Succeeded)
                return Ok(projectResponse);
            return BadRequest(projectResponse);

        }

        [HttpDelete("DeleteProject")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "DeleteProject", Description = "Delete a Project", GroupName = "Projects")]
        public IActionResult DeleteProject(int id)
        {
            var projectResponse = _projectServices.DeleteProject(id);

            if (projectResponse.Succeeded)
                return Ok(projectResponse);
            return BadRequest(projectResponse);

        }


        [HttpPut("AddEmployeeToProject")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(ApiResponse<AllDataProjectViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<AllDataProjectViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "AddEmployeeToProject", Description = "Add Employee to Project", GroupName = "Projects")]
        public ActionResult AddEmployeeToProject(int employeeId, int projectId)
        {
            var projectResponse = _projectServices.AddEmployeeToProject(employeeId, projectId);

            if (projectResponse.Succeeded)
                return Ok(projectResponse);
            return BadRequest(projectResponse);

        }

        [HttpDelete("RemoveEmployeeFromProject")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "RemoveEmployeeFromProject", Description = "Remove Employee from Project", GroupName = "Projects")]
        public ActionResult RemoveEmployeeFromProject(int employeeId, int projectId)
        {
            var projectResponse = _projectServices.RemoveEmployeeFromProject(employeeId, projectId);

            if (projectResponse.Succeeded)
                return Ok(projectResponse);
            return BadRequest(projectResponse);
        }

    }
}
