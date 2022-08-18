using EmployeesData.IRepositories;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.ViewModels;
using System;
using System.Linq;
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
        public EmployeeController(IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
        }

        [HttpGet("GetProfileData")]
        public IActionResult GetProfileData()
        {
            try
            {
                var user = GetCurrentUser();
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("UpdateProfileData")]
        public IActionResult UpdateProfileData(UserEditViewModel user)
        {
            try
            {
                int id = GetCurrentUser().Id;
                var userToUpdate = _userServices.GetUserById(id);

                if (userToUpdate == null)
                    return NotFound("User with Id = " + id.ToString() + " not found");

                if (user == null)
                    return BadRequest();

                return Ok(_userServices.UpdateUser(user, id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ViewProjects")]
        public IActionResult GetEmployeeProjects()
        {
            try
            {
                int userId = GetCurrentUser().Id;
                var projects = _projectServices.GetEmployeeProjects(userId);
                return Ok(projects);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
        [HttpPost("CreateTask")]
        public ActionResult CreateTask(ProjectTaskEditViewModel task)
        {
            //Create these tasks and assign to other employees that are part of the project
            return null;
        }

        [HttpPut("ChangeTaskStatusToCompleted")]
        public IActionResult ChangeTaskStatusToCompleted(int TaskId)
        {
            try
            {
                int UserId = GetCurrentUser().Id;
                var taskToUpdate = _taskServices.GetTaskByIdAndUserId(TaskId, UserId);

                if (taskToUpdate == null)
                    return NotFound("Task with Id = " + TaskId.ToString() + " not found");

                return Ok(_taskServices.ChangeTaskStatus(taskToUpdate,TaskId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("GetTasks")]
        public IActionResult GetTasks()
        {
            try
            {
                int UserId = GetCurrentUser().Id;
                var tasks = _taskServices.GetTasks(UserId);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private UserViewModel GetCurrentUser()
        {
            var loggedInUser = _userServices.GetLoggedInUser(GetUserUsername().Username);
            return loggedInUser;
        }

        private UserViewModel GetUserUsername()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserViewModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value
                };
            }
            return null;
        }


    }
}
