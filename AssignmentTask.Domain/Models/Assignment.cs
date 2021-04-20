using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Domain.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } 
        public string Description { get; set; } 
        public string Path { get; set; }
        public string Signature { get; set; }
        public string Owner { get; set; }

    }
}
