using AutoMapper;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.AutoMapper
{
    public class ViewModelToDomainProfile:Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<StudentViewModel, Student>();
            CreateMap<TeacherViewModel, Teacher>();
            CreateMap<TaskViewModel, Task>();
            CreateMap<AssignmentViewModel, Assignment>();
        }
    }
}
