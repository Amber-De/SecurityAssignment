using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssignmentTask.Domain.Models;

namespace AssignmentTask.Domain.Interfaces
{
    public interface IAssignmentsRepository
    {
        void AddAssignment(Assignment assignment);
        IQueryable<Assignment> ListAssignments(Guid taskId);
        Assignment GetAssignment(Guid studentId, Guid taskId);
        Assignment GetAssignmentById(Guid assignmentId);
    }
}
