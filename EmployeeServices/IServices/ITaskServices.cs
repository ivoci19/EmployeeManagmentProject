using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.IServices
{
    public interface ITaskServices
    {
        public ProjectTasksViewModel GetTaskById(int id);
        public ProjectTasksViewModel CreateTask(ProjectTasksViewModel task);
        public ProjectTasksViewModel UpdateTask(ProjectTasksViewModel task);
        public bool DeleteTask(ProjectTasksViewModel task);
        public List<ProjectTasksViewModel> GetAllProjects();
    }
}
