using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddAssignment(AssignmentViewModel assignmentModel)
        {
            _assignmentsRepo.AddAssignment(_autoMapper.Map<Assignment>(assignmentModel));
        }

        public AssignmentViewModel GetAssignment(Guid studentId, Guid taskId)
        {
            if(studentId != null && taskId != null)
            {
                var assignment = _assignmentsRepo.GetAssignment(studentId,taskId);
                return _autoMapper.Map<AssignmentViewModel>(assignment);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public IQueryable<AssignmentViewModel> ListAssignments(Guid TaskId)
        {
            return _assignmentsRepo.ListAssignments(TaskId).ProjectTo<AssignmentViewModel>(_autoMapper.ConfigurationProvider);
        }
    }
}
