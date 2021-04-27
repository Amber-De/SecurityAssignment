using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Application.Services
{
    public class CommentsService : ICommentsService
    {
        private ICommentsRepository _commentsRepo;
        private IMapper _mapper;

        public CommentsService(ICommentsRepository commentsRepo, IMapper mapper)
        {
            _commentsRepo = commentsRepo;
            _mapper = mapper;
        }
        public void AddComment(CommentViewModel comment)
        {
            _commentsRepo.AddComment(_mapper.Map<Comment>(comment));
        }

        public IQueryable<CommentViewModel> ListComments(Guid studentId, Guid teacherId, Guid assignmentId)
        {
            return _commentsRepo.ListComments(studentId,teacherId,assignmentId).ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider);
        }
    }
}
