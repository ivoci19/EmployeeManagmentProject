﻿using AutoMapper;
using EmployeesData.IRepositories;
using EmployeesData.Models;
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

        private readonly IProjectTaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskServices(IProjectTaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }


        public ProjectTaskViewModel CreateTask(ProjectTaskEditViewModel taskVm)
        {
            ProjectTask task = _mapper.Map<ProjectTask>(taskVm);
            _taskRepository.SaveTask(task);
            return _mapper.Map<ProjectTaskViewModel>(task);
        }

        public bool DeleteTask(int id)
        {
            ProjectTask task = _taskRepository.ProjectTasks.FirstOrDefault(u => u.Id == id);

            if (task != null)
            {
                return _taskRepository.DeleteTask(id);
            }
            return false;
        }

        public IEnumerable<ProjectTaskViewModel> GetAllTasks()
        {
            var tasks = _taskRepository.ProjectTasks;

            return _mapper.Map<IEnumerable<ProjectTaskViewModel>>(tasks); 
        }

        public ProjectTaskViewModel GetTaskById(int id)
        {
            ProjectTask task = _taskRepository.GetTaskById(id);
            return _mapper.Map<ProjectTaskViewModel>(task);
        }

        public ProjectTaskViewModel UpdateTask(ProjectTaskEditViewModel taskData, int id)
        {
            ProjectTask task = _taskRepository.ProjectTasks.FirstOrDefault(e => e.Id == id);

            if (task != null)
            {
                _mapper.Map(taskData, task);
                _taskRepository.SaveTask(task);
                var taskVm = _mapper.Map<ProjectTaskViewModel>(task);
                return taskVm;
            }

            return null;
        }
    }
}