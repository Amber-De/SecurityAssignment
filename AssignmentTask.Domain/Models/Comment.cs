using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AssignmentTask.Domain.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CommentId { get; set; }
        public string CommentArea { get; set; }

        [ForeignKey("Student")]
        public Guid StudentID { get; set; }
        public virtual Student Student { get; set; }

        [ForeignKey("Teacher")]
        public Guid TeacherID { get; set; }
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("Assignment")]
        public Guid AssignmentID { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}
