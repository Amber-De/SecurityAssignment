using AssignmentTask.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.Interfaces
{
    public interface ITeachersService
    {
        void AddTeacher(TeacherViewModel t);
    }
}
