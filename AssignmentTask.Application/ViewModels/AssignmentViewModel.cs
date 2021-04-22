using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssignmentTask.Application.ViewModels
{
    public class AssignmentViewModel
    {
        [Required(ErrorMessage = "Please input name of file")]
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Signature { get; set; }

        public StudentViewModel Student { get; set; }
        public TaskViewModel Task {get; set;}
    }
}
