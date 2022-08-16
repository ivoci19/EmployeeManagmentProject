﻿using SharedModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.IServices
{
    public interface ITaskServices
    {
        public ProjectTaskViewModel GetTaskById(int id);
        public ProjectTaskViewModel CreateTask(ProjectTaskEditViewModel task);
        public ProjectTaskViewModel UpdateTask(ProjectTaskEditViewModel taskData, int id);
        public bool DeleteTask(int id);
        public IEnumerable<ProjectTaskViewModel> GetAllTasks();
    }
}
