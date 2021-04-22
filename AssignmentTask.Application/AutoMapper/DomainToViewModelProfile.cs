using AutoMapper;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.AutoMapper
{
    public class DomainToViewModelProfile: Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<StudentViewModel, Student>();
            CreateMap<TeacherViewModel, Teacher>();
            CreateMap<TaskViewModel, Task>();
            CreateMap<Assignment, AssignmentViewModel>();
        }

    }
}
