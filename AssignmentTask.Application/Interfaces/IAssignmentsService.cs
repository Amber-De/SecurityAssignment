using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Application.Interfaces
{
    public interface IAssignmentsService
    {
        AssignmentViewModel GetAssignment(Guid studentId, Guid TaskId);
        void AddAssignment(AssignmentViewModel assignment);
        IQueryable<AssignmentViewModel> ListAssignments(Guid TaskId);
    }
}
