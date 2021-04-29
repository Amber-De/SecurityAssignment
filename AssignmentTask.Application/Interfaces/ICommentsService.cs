using AssignmentTask.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Application.Interfaces
{
    public interface ICommentsService
    {
        void AddComment(CommentViewModel comment);
        IQueryable<CommentViewModel> ListComments( Guid assignmentId);
    }
}
