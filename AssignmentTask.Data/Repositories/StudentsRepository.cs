using AssignmentTask.Data.Context;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Data.Repositories
{
    public class StudentsRepository: IStudentsRepository
    {
        TaskDbContext _context;

        public StudentsRepository(TaskDbContext context)
        {
            _context = context;
        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public Student GetStudent(string email)
        {
            return _context.Students.SingleOrDefault(x => x.Email == email);
        }

        public Student GetStudentById(Guid studentId)
        {
            return _context.Students.SingleOrDefault(x => x.Id == studentId);
        }
    }
}
