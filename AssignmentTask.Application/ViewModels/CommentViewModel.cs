using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string CommentArea { get; set; }
        public Guid StudentID { get; set; }
        public Guid TeacherID { get; set; }
        public Guid AssignmentID { get; set; }
    }
}
