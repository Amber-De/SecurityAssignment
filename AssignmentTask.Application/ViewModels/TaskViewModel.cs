using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.ViewModels
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
