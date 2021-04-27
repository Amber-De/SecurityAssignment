using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Create(CommentViewModel comment, Guid Id)
        {
            string loggedInUser = User.Identity.Name;

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
                var assignment = _assignmentsService.GetAssignmentById(Id);
                comment.StudentID = assignment.StudentId;
            }
      
            comment.AssignmentID = Id;

            _commentsService.AddComment(comment);
            return Redirect("/Tasks/List");
        }

        public IActionResult List(Guid Id, Guid StudentId)
        {
            string loggedInUser = User.Identity.Name;

            if (User.IsInRole("TEACHER"))
            {
                var teacher = _teachersService.GetTeacherId(loggedInUser);
                var student = _studentsService.GetStudentById(StudentId);
                var comments = _commentsService.ListComments(StudentId, teacher.Id, Id); //id = assignmentID
                ViewBag.teacher = teacher;
                ViewBag.student = student;
                return View(comments);
            }
            else
            {
                var student = _studentsService.GetStudent(loggedInUser);
                var comments = _commentsService.ListComments(student.Id, student.TeacherID, Id);
                var teacher = _teachersService.GetTeacherById(student.TeacherID);
                ViewBag.teacher = teacher;
                ViewBag.student = student;
                return View(comments);
            }
        }
    }
}
