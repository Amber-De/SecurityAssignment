using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace WebApplication1.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;
        private readonly ITeachersService _teachersService;
        private readonly IStudentsService _studentsService;
        public TasksController(ITasksService tasksService, ITeachersService teachersService, IStudentsService studentsService)
        {
            _tasksService = tasksService;
            _teachersService = teachersService;
            _studentsService = studentsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "TEACHER")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "TEACHER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskViewModel task)
        {
            string loggedInUser = User.Identity.Name;
            var teacher = _teachersService.GetTeacherId(loggedInUser);

            task.Description = HtmlEncoder.Default.Encode(task.Description);
            if (task != null )
            {
                if(task.Deadline > DateTime.Now) {

                    task.TeacherId = teacher.Id;
                    _tasksService.CreateTask(task);

                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["feedback"] = "Invalid date";
                    return RedirectToAction("Create");
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public IActionResult List()
        {
            string loggedInUser = User.Identity.Name;

            if (User.IsInRole("TEACHER"))
            {     
                var teacher = _teachersService.GetTeacherId(loggedInUser);

                if (loggedInUser != null)
                {
                    Guid teachId = teacher.Id;
                    var list = _tasksService.GetTasksList(teachId);
                    return View(list);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            else
            {
                var student = _studentsService.GetStudent(loggedInUser);
                Guid id = student.TeacherID;
                var list = _tasksService.GetTasksList(id);

                return View(list);
            }
            
        }
    }
}
