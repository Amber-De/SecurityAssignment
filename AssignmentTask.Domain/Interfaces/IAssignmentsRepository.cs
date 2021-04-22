using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssignmentTask.Domain.Models;

namespace AssignmentTask.Domain.Interfaces
{
    public interface IAssignmentsRepository
    {
        Guid AddAssignment(Assignment assignment);

    }
}
