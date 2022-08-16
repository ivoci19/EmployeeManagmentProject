using EmployeesData.IRepositories;
using EmployeesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public List<ProjectTask> ProjectTasks
        {
            get { return _applicationDbContext.ProjectTasks.ToList(); }
        }

        public bool DeleteProject(int prjectId)
        {
            throw new NotImplementedException();
        }

        public void SaveTask(ProjectTask task)
        {
            throw new NotImplementedException();
        }
    }
}
