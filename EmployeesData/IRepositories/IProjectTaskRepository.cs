using EmployeesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.IRepositories
{
    public interface IProjectTaskRepository
    {
        List<ProjectTask> ProjectTasks { get; }
        public void SaveTask(ProjectTask projectTask);
        public bool DeleteTask(int id);
        public ProjectTask GetTaskById(int id);
    }
}
