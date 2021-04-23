using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AssignmentTask.Domain.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        public string Description { get; set;  }
        [Required]
        public DateTime Deadline { get; set; }

        [ForeignKey("Teacher")]
        public Guid TeacherID { get; set; }
        public virtual Teacher Teacher { get; set; }

    }
}
