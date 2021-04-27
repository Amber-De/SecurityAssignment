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
    public class CommentsRepository : ICommentsRepository
    {
        TaskDbContext _context;

        public CommentsRepository(TaskDbContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public IQueryable<Comment> ListComments(Guid studentId, Guid teacherId, Guid assignmentId)
        {
            return _context.Comments.Include(x => x.Assignment).Where(x => x.StudentID == studentId).Where(x => x.TeacherID == teacherId);
        }
    }
}
