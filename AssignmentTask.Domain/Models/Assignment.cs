﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AssignmentTask.Domain.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string FileName { get; set; } 
        public string Description { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string Signature { get; set; }
        [ForeignKey("Student")]
        public string StudentID { get; set; }
        [ForeignKey("Task")]
        public string TaskID { get; set; }

    }
}
