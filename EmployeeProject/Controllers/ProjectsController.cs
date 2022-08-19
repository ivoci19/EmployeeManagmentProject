using EmployeeServices.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.ViewModels;
using System;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IProjectServices _projectServices;
        private readonly ITaskServices _taskServices;
        public ProjectsController(IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
        }

        [HttpGet("GetAllProjects")]
        public IActionResult GetAllProjects()
        {
            try
            {
                var projects = _projectServices.GetAllProjects();
                return Ok(projects);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("GetProject{id:int}")]
        public IActionResult GetProject(int id)
        {
            try
            {
                var project = _projectServices.GetProjectById(id);
                return Ok(project);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateProject")]
        public ActionResult CreateProject(ProjectEditViewModel project)
        {
            try
            {
                if (project == null)
                    return BadRequest();

                var createdProject = _projectServices.CreateProject(project);

                return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut("UpdateProject{id:int}")]
        public IActionResult UpdateProject(int id, ProjectEditViewModel project)
        {
            try
            {
                var projectToUpdate = _projectServices.GetProjectById(id);

                if (projectToUpdate == null)
                    return NotFound("Project with Id = " + id.ToString() + " not found");

                if (project == null)
                    return BadRequest();

                return Ok(_projectServices.UpdateProject(project, id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteProject{id:int}")]
        public IActionResult DeleteProject(int id)
        {
            try
            {
                var project = _projectServices.GetProjectById(id);
                if (project == null)
                    return NotFound("Project with Id = " + id.ToString() + " not found");

                _projectServices.DeleteProject(id);
                return Ok(project);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("AddEmployeeToProject")]
        public ActionResult AddEmployeeToProject(int EmployeeId, int ProjectId)
        {
            try
            {
                var employee = _userServices.GetUserById(EmployeeId, true, true);
                var project = _projectServices.GetProjectById(ProjectId);

                if (employee == null)
                    return NotFound("Employee with Id = " + EmployeeId.ToString() + " not found");
                if (project == null)
                    return NotFound("Employee with Id = " + EmployeeId.ToString() + " not found");

                return Ok(_projectServices.AddEmployeeToProject(EmployeeId, ProjectId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("RemoveEmployeeFromProject")]
        public ActionResult RemoveEmployeeFromProject(int EmployeeId, int ProjectId)
        {
            try
            {
                var employee = _userServices.GetUserById(EmployeeId, true, true);
                var project = _projectServices.GetProjectById(ProjectId);

                if (employee == null)
                    return NotFound("Employee with Id = " + EmployeeId.ToString() + " not found");
                if (project == null)
                    return NotFound("Employee with Id = " + EmployeeId.ToString() + " not found");

                return Ok(_projectServices.RemoveEmployeeFromProject(EmployeeId,ProjectId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
