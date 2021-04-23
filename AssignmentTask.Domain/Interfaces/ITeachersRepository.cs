using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Domain.Interfaces
{
    public interface ITeachersRepository
    {
        void AddTeacher(Teacher teacher);
    }
}
