using AssignmentTask.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Application.Interfaces
{
    public interface ITasksService
    {
        IQueryable<TaskViewModel> GetTasksList(Guid teacherId);
        void CreateTask(TaskViewModel task);

        TaskViewModel GetTask(Guid taskId);
    }
}
