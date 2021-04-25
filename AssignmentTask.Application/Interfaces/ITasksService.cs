using AssignmentTask.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.Interfaces
{
    public interface ITasksService
    {
        void CreateTask(TaskViewModel task);
    }
}
