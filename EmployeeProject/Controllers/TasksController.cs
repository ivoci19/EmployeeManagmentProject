using EmployeeProject.Helper;
using EmployeeProject.Helpers;
using EmployeesData.Models;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class TasksController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IProjectServices _projectServices;
        private readonly ITaskServices _taskServices;
        private readonly IIdentityHelper _identityHelper;
        public TasksController(IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices, IIdentityHelper identityHelper)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
            _identityHelper = identityHelper;
        }

        [HttpGet("GetAllTasks")]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetAllTasks", Description = "Get all Tasks", GroupName = "Tasks")]
        public IActionResult GetAllTasks()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var taskResponse = _taskServices.GetAllTasks(loggedInUser);

            if (taskResponse.Succeeded)
                return Ok(taskResponse);
            return BadRequest(taskResponse);

        }


        [HttpGet("GetTask")]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "GetTask", Description = "Get Task by Id", GroupName = "Tasks")]
        public ActionResult<ProjectTask> GetTask(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var taskResponse = _taskServices.GetTaskById(id, loggedInUser);

            if (taskResponse.Succeeded)
                return Ok(taskResponse);
            return BadRequest(taskResponse);

        }

        [HttpPost("CreateTask")]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "CreateTask", Description = "Create new Task", GroupName = "Tasks")]
        public ActionResult CreateTask(ProjectTaskEditViewModel task)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var taskResponse = _taskServices.CreateTask(task, loggedInUser);

            if (taskResponse.Succeeded)
                return Ok(taskResponse);
            return BadRequest(taskResponse);

        }

        [HttpPut("UpdateTask")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "UpdateTask", Description = "Update a Task", GroupName = "Tasks")]
        public IActionResult UpdateTask(int id, ProjectTaskEditViewModel task)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var errors = ModelStateHelper.GetErrors(modelErrors);
                return BadRequest(ApiResponse<ProjectTaskViewModel>.ApiFailResponse(ErrorCodes.BAD_REQUEST, errors));
            }


            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var taskResponse = _taskServices.UpdateTask(task, id, loggedInUser);

            if (taskResponse.Succeeded)
                return Ok(taskResponse);
            return BadRequest(taskResponse);
        }

        [HttpDelete("DeleteTask")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "DeleteTask", Description = "Delete a Task", GroupName = "Tasks")]
        public IActionResult DeleteTask(int id)
        {
            var taskResponse = _taskServices.DeleteTask(id);

            if (taskResponse.Succeeded)
                return Ok(taskResponse);
            return BadRequest(taskResponse);
        }

        [HttpPut("ChangeTaskStatus")]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "ChangeTaskStatus", Description = "Change the status of a task", GroupName = "Tasks")]
        public IActionResult ChangeTaskStatus(int taskId, TaskStatusEnum status)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var taskResponse = _taskServices.ChangeTaskStatus(taskId, status, loggedInUser);

            if (taskResponse.Succeeded)
                return Ok(taskResponse);
            return BadRequest(taskResponse);

        }

        [HttpPut("AssignTaskToEmployee")]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ProjectTaskViewModel>), (int)HttpStatusCode.BadRequest)]
        [Display(Name = "AssignTaskToEmployee", Description = "Assign a task to an employee", GroupName = "Tasks")]
        public IActionResult AssignTaskToEmployee(int taskId, int employeeId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInUser = _identityHelper.GetCurrentUser(identity);

            var taskResponse = _taskServices.AssignTaskToEmployee(taskId, employeeId, loggedInUser);

            if (taskResponse.Succeeded)
                return Ok(taskResponse);
            return BadRequest(taskResponse);

        }


    }
}
