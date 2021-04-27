using AssignmentTask.Data.Context;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Data.Repositories
{
    public class AssignmentsRepository : IAssignmentsRepository
    {
        TaskDbContext _context;
        public AssignmentsRepository(TaskDbContext context)
        {
            _context = context;
        }

        public void AddAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            _context.SaveChanges();
        }

        public Assignment GetAssignment(Guid studentId, Guid taskId)
        {
           return _context.Assignments.Where(x => x.TaskID == taskId).SingleOrDefault(x => x.StudentID == studentId);
        }

        public IQueryable<Assignment> ListAssignments(Guid taskId)
        {
            return _context.Assignments.Where(x => x.TaskID == taskId);
        }
    }
}
