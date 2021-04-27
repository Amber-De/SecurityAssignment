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
    public class StudentsService : IStudentsService
    {
        private IStudentsRepository _studentsRepo;
        private IMapper _autoMapper;

        public StudentsService(IStudentsRepository studentsRepo, IMapper mapper)
        {
            _studentsRepo = studentsRepo;
            _autoMapper = mapper;
        }
        public void AddStudent(StudentViewModel student)
        {
            _studentsRepo.AddStudent(_autoMapper.Map<Student>(student));
        }

        public StudentViewModel GetStudent(string email)
        {
            var e = _studentsRepo.GetStudent(email);
            if (e != null)
            {
                var result = _autoMapper.Map<StudentViewModel>(e);
                return result;
            }
            else
            {
                return null;
            }
        }

        public StudentViewModel GetStudentById(Guid studentId)
        {
            var e = _studentsRepo.GetStudentById(studentId);
            if (e != null)
            {
                var result = _autoMapper.Map<StudentViewModel>(e);
                return result;
            }
            else
            {
                return null;
            };
        }
    }
}
