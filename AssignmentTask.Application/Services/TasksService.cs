using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.Services
{
    public class TasksService: ITasksService
    {
        private ITasksRepository _tasksRepo;
        private IMapper _autoMapper;
        

        public TasksService(ITasksRepository tasksRepo, IMapper autoMapper)
        {
            _tasksRepo = tasksRepo;
            _autoMapper = autoMapper;
           
        }
               
        public void CreateTask(TaskViewModel task)
        {
            _tasksRepo.CreateTask(_autoMapper.Map<Task>(task));
        }
    }
}
