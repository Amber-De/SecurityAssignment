using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Domain.Interfaces
{
    public interface ITasksRepository
    {
        IQueryable<Task> GetTasksList();
        void CreateTask(Task task);
    }
}
