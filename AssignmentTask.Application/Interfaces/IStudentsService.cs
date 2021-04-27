using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.Interfaces
{
    public interface IStudentsService
    {
        void AddStudent(StudentViewModel student);
        StudentViewModel GetStudent(string email);

        StudentViewModel GetStudentById(Guid studentId);
    }
}
