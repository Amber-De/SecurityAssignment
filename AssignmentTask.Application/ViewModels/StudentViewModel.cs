using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssignmentTask.Application.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Guid TeacherID { get; set; }

        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
