using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.ViewModels
{
    public class TaskViewModel
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TeacherViewModel Teacher { get; set; }
    }
}
