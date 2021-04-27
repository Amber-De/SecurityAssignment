using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.Services
{
    public class TeachersService : ITeachersService
    {
        private ITeachersRepository _teachersRepo;
        private IMapper _autoMapper;

        public TeachersService(ITeachersRepository repo, IMapper mapper)
        {
            _teachersRepo = repo;
            _autoMapper = mapper;
        }
        public void AddTeacher(TeacherViewModel t)
        {
            _teachersRepo.AddTeacher(_autoMapper.Map<Teacher>(t));
        }

        public TeacherViewModel GetTeacherById(Guid teacherId)
        {
            var e = _teachersRepo.GetTeacherById(teacherId);
            if (e != null)
            {
                var result = _autoMapper.Map<TeacherViewModel>(e);
                return result;
            }
            else
            {
                return null;
            }
        }

        public TeacherViewModel GetTeacherId(string email)
        {
            var e = _teachersRepo.GetTeacherId(email);
            if(e != null)
            {
                var result = _autoMapper.Map<TeacherViewModel>(e);
                return result;
            }
            else
            {
                return null;
            }
          
        }
    }
}
