using EmployeeServices.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enum;
using SharedModels.ViewModels;
using System;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IProjectServices _projectServices;
        private readonly ITaskServices _taskServices;
        public TasksController(IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
        }
        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
        {
            try
            {
                var tasks = _taskServices.GetAllTasks();
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("GetTask{id:int}")]
        public IActionResult GetTask(int id)
        {
            try
            {
                var task = _taskServices.GetTaskById(id);
                return Ok(task);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateTask")]
        public ActionResult CreateTask(ProjectTaskEditViewModel task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                var createdTask = _taskServices.CreateTask(task);

                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut("UpdateTask{id:int}")]
        public IActionResult UpdateTask(int id, ProjectTaskEditViewModel task)
        {
            try
            {
                var taskToUpdate = _taskServices.GetTaskById(id);

                if (taskToUpdate == null)
                    return NotFound("Task with Id = " + id.ToString() + " not found");

                if (task == null)
                    return BadRequest();

                return Ok(_taskServices.UpdateTask(task, id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteTask{id:int}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                var task = _taskServices.GetTaskById(id);
                if (task == null)
                    return NotFound("Task with Id = " + id.ToString() + " not found");

                _taskServices.DeleteTask(id);
                return Ok(task);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("ChangeTaskStatus{id:int}")]
        public IActionResult ChangeTaskStatus(int id, TaskStatusEnum status)
        {
            try
            {
                var taskToUpdate = _taskServices.GetTaskById(id);

                if (taskToUpdate == null)
                    return NotFound("Task with Id = " + id.ToString() + " not found");

                return Ok(_taskServices.ChangeTaskStatus(id, status));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
