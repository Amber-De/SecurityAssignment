using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;
        private readonly ITeachersService _teachersService;

        public TasksController(ITasksService tasksService, ITeachersService teachersService)
        {
            _tasksService = tasksService;
            _teachersService = teachersService;
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
        public IActionResult Create(TaskViewModel task)
        {
            string loggedInUser = User.Identity.Name;
            var teacher = _teachersService.GetTeacherId(loggedInUser);
            
            if(task != null)
            {
                if(task.Deadline > DateTime.Now) {

                    task.TeacherId = teacher.Id;
                    _tasksService.CreateTask(task);

                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["message"] = "Invalid date";
                    return RedirectToAction("Tasks", "Create");
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
          
        }
    }
}
