using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TaskViewModel GetTask(Guid taskId)
        {
            Task task = _tasksRepo.GetTask(taskId);
            return _autoMapper.Map<TaskViewModel>(task);
        }

        public IQueryable<TaskViewModel> GetTasksList(Guid teacherId)
        {
           return _tasksRepo.GetTasksList(teacherId).ProjectTo<TaskViewModel>(_autoMapper.ConfigurationProvider);
        }
    }
}
