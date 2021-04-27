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
    public class TeachersRepository : ITeachersRepository
    {
        TaskDbContext _context;

        public TeachersRepository(TaskDbContext context)
        {
            _context = context;
        }

        public void AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        public Teacher GetTeacherById(Guid teacherId)
        {
            return _context.Teachers.SingleOrDefault(x => x.Id == teacherId);
        }

        public Teacher GetTeacherId(string email)
        {
            return  _context.Teachers.SingleOrDefault(x => x.Email == email);
        }
    }
}
