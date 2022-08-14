using EmployeesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.IRepositories
{
    public interface ITaskRepository
    {
        List<ProjectTask> ProjectTasks { get; }
    }
}
