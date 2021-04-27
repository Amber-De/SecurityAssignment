using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Domain.Interfaces
{
    public interface ITasksRepository
    {
        IQueryable<Task> GetTasksList(Guid teacherId);
        void CreateTask(Task task);
        Task GetTask(Guid taskId);
    }
}
