using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class AssignmentsController: Controller
    {
        private readonly IAssignmentsService _assignmentsService;
        private readonly IStudentsService _studentsService;
        private readonly ITasksService _tasksService;
        private readonly IWebHostEnvironment _host;
        private readonly ILogger _logger;

        public AssignmentsController(IAssignmentsService assignmentsService, IWebHostEnvironment host, ILogger<AssignmentsController> logger,
            IStudentsService studentsService, ITasksService tasksService)
        {
            _assignmentsService = assignmentsService;
            _studentsService = studentsService;
            _tasksService = tasksService;
            _host = host;
            _logger = logger;
        }

        public IActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            string loggedInUser = User.Identity.Name;
            var student = _studentsService.GetStudent(loggedInUser);
            var task = _tasksService.GetTask(id);

            var a = _assignmentsService.GetAssignment(student.Id, id);
            if (a != null)
            {
                return RedirectToAction("StudentAssignment" , new { studentId = student.Id ,  taskId = id});
            }else if(DateTime.UtcNow > task.Deadline)
            {
                return RedirectToAction("StudentAssignment", new { studentId = student.Id, taskId = id });
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(Guid id, IFormFile file, AssignmentViewModel assignment)
        {
            string uniqueFileName = "";
            assignment.Description = HtmlEncoder.Default.Encode(assignment.Description);

            if (Path.GetExtension(file.FileName) == ".pdf")
            {
                byte[] whitelist = new byte[] { 37, 80, 68, 70 };

                if (file != null)
                {
                    MemoryStream userFile = new MemoryStream();

                    using (var f = file.OpenReadStream())
                    {
                        //Reading a number of bytes at the same type(4) and not skipping any(0)
                        byte[] buffer = new byte[4];
                        f.Read(buffer, 0, 4);

                        for (int i = 0; i < whitelist.Length; i++)
                        {
                            if (whitelist[i] != buffer[i])
                            {
                                ModelState.AddModelError("file", "File is not valid nor accepted");
                                return View();
                            }
                        }
                        f.Position = 0;

                        uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        assignment.Path = uniqueFileName;
                        string absolutePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ValueableFiles")).Root + uniqueFileName;

                        using (FileStream fsOut = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            f.CopyTo(fsOut);
                        }

                        f.CopyTo(userFile);
                        f.Close();
                    }
                    
                    string loggedInUser = User.Identity.Name;
                    var student = _studentsService.GetStudent(loggedInUser);
                    var task = _tasksService.GetTask(id);

                    //var a =  _assignmentsService.GetAssignment(student.Id, task.Id);
                   
                    assignment.StudentId = student.Id;
                    assignment.TaskId = id;
                    _assignmentsService.AddAssignment(assignment);
                    return Redirect("/Home/Index");                     
                }
                TempData["warning"] = "File is null";
                return RedirectToAction("Create");
            }
            TempData["warning"] = "Only pdf files";
            return RedirectToAction("Create");
        }

        public IActionResult ViewAssignments(Guid id)
        {
            var task = _tasksService.GetTask(id);

            if (id != null)
            {
                var list = _assignmentsService.ListAssignments(id);
                ViewBag.task = task;
                return View(list);
            }
            else
            {
                throw new NullReferenceException();
            } 
        }

        public IActionResult StudentAssignment(Guid studentId, Guid taskId)
        {
            var task = _tasksService.GetTask(taskId);
            var student = _studentsService.GetStudentById(studentId);

            if(studentId != null && taskId != null)
            {
                var  assignment = _assignmentsService.GetAssignment(studentId, taskId);
                ViewBag.task = task;
                ViewBag.student = student;
                return View(assignment);
            }
            else
            {
                throw new NullReferenceException();
            }
           
        }

    }
}
