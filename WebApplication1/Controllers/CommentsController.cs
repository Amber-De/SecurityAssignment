using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApplication1.ActionFilters;

namespace WebApplication1.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentsService;
        private readonly IAssignmentsService _assignmentsService;
        private readonly IStudentsService _studentsService;
        private readonly ITeachersService _teachersService;
        public CommentsController(IAssignmentsService assignmentsService,
           IStudentsService studentsService, ITeachersService teachersService, ICommentsService commentsService)
        {
            _assignmentsService = assignmentsService;
            _studentsService = studentsService;
            _teachersService = teachersService;
            _commentsService = commentsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(Guid Id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken][AssignmentOwnerAuthorize]
        public IActionResult Create(CommentViewModel comment, Guid id)
        {
            string loggedInUser = User.Identity.Name;
            comment.CommentArea = HtmlEncoder.Default.Encode(comment.CommentArea);
            if (User.IsInRole("STUDENT"))
            {
                var student = _studentsService.GetStudent(loggedInUser);
                comment.TeacherID = student.TeacherID;
                comment.StudentID = student.Id;
            }
            else
            {
                var teacher = _teachersService.GetTeacherId(loggedInUser);
                comment.TeacherID = teacher.Id;
                var assignment = _assignmentsService.GetAssignmentById(id);
                comment.StudentID = assignment.StudentId;
            }
      
            comment.AssignmentID = id;

            _commentsService.AddComment(comment);
            return Redirect("/Tasks/List");
        }

        [AssignmentOwnerAuthorize]
        public IActionResult List(Guid id)
        {
            var assignment = _assignmentsService.GetAssignmentById(id);
            var comments = _commentsService.ListComments(id);
            ViewBag.student = assignment.Student;
            ViewBag.teacher = assignment.Student.Teacher;

            return View(comments);
        }
    }
}
