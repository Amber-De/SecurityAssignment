using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.Services
{
    public class TeachersService : ITeachersService
    {
        ITeachersRepository _teachersRepo;

        public TeachersService(ITeachersRepository repo)
        {
            _teachersRepo = repo;
        }
        public void AddStudent(StudentViewModel s)
        {
          
        }
    }
}
