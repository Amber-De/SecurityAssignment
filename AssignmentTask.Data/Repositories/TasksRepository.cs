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
        public Guid AddTask(Task task)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Task> GetTasksList()
        {
            throw new NotImplementedException();
        }
    }
}
