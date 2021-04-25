using AssignmentTask.Data.Context;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        TaskDbContext _context;


        public TasksRepository(TaskDbContext context)
        {
            _context = context;
        }

        public void CreateTask(Task task)
        {

            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public IQueryable<Task> GetTasksList()
        {
            throw new NotImplementedException();
        }
    }
}
