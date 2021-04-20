using AssignmentTask.Application.Interfaces;
using AssignmentTask.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTask.Application.Services
{
    public class AssignmentsService: IAssignmentsService
    {
        private IAssignmentsRepository _assignmentsRepo;
        private IMapper _autoMapper;

        public AssignmentsService(IAssignmentsRepository assignmentsRepo, IMapper autoMapper)
        {
            _assignmentsRepo = assignmentsRepo;
            _autoMapper = autoMapper;
        }
    }
}
