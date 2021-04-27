using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Domain.Interfaces
{
    public interface IStudentsRepository
    {
        void AddStudent(Student student);
        Student GetStudent(string email);

        Student GetStudentById(Guid studentId);
    } 
}
