﻿using AssignmentTask.Application.Interfaces;
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
    }
}