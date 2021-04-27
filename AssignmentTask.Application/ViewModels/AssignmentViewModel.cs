using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssignmentTask.Application.ViewModels
{
    public class AssignmentViewModel
    {
        public  Guid Id {get; set;}
        [Required(ErrorMessage = "Please input name of file")]
        public string FileName { get; set; }
        public string Description { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string Signature { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid TaskId {get; set;}
    }
}
