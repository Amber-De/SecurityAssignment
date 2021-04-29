using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Domain.Interfaces
{
    public interface ICommentsRepository
    {
        void AddComment(Comment comment);
        IQueryable<Comment> ListComments(Guid assignmentId);

    }
}
