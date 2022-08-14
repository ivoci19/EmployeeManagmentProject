using EmployeesData.IRepositories;
using EmployeeServices.IServices;
using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Services
{
    public class TaskServices : ITaskServices
    {

        private readonly ITaskRepository _taskRepository;
        public TaskServices(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public ProjectTasksViewModel CreateTask(ProjectTasksViewModel task)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTask(ProjectTasksViewModel task)
        {
            throw new NotImplementedException();
        }

        public List<ProjectTasksViewModel> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public ProjectTasksViewModel GetTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public ProjectTasksViewModel UpdateTask(ProjectTasksViewModel task)
        {
            throw new NotImplementedException();
        }
    }
}
