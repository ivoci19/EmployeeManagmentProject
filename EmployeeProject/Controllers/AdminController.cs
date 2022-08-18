using EmployeesData.Models;
using EmployeesData.Repositories;
using EmployeeServices.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enum;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    //Ka te beje kjo? qe e kisha si koment
    public class AdminController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IProjectServices _projectServices;
        private readonly ITaskServices _taskServices;
        public AdminController( IUserServices userServices, IProjectServices projectServices, ITaskServices taskServices)
        {
            _userServices = userServices;
            _projectServices = projectServices;
            _taskServices = taskServices;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userServices.GetAllUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("GetUser{id:int}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userServices.GetUserById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateUser")]
        public ActionResult CreateUser(UserEditViewModel user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                bool duplicateUsername = _userServices.GetUserByUsername(user.Username);
                if (duplicateUsername)
                {
                    ModelState.AddModelError("username", "Username " + user.Username + " is already in use.");
                    return BadRequest(ModelState);
                }

                var createdUser = _userServices.CreateUser(user);

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("UpdateUser{id:int}")]
        public IActionResult UpdateUser(int id, UserEditViewModel user)
        {
            try
            {
                var userToUpdate = _userServices.GetUserById(id);

                if (userToUpdate == null)
                    return NotFound("User with Id = " + id.ToString() + " not found");

                if (user == null)
                    return BadRequest();

                return Ok(_userServices.UpdateUser(user,id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteUser{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _userServices.GetUserById(id);
                if (user == null)
                    return NotFound("User with Id = " + id.ToString() + " not found");

                _userServices.DeleteUser(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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

        [HttpPost("AddEmployeeToProject")]
        public ActionResult AddEmployeeToProject(int EmployeeId, int ProjectId)
        {
            return null;
        }

        [HttpDelete("RemoveEmployeeFromProject")]
        public ActionResult RemoveEmployeeFromProject(int EmployeeId, int ProjectId)
        {
            return null;
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
