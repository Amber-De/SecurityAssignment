using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApplication1.ActionFilters;
using WebApplication1.Models;
using WebApplication1.Utility;

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

        [HttpGet][Authorize]
        public IActionResult Create(Guid TaskId)
        {
            string loggedInUser = User.Identity.Name;
            var student = _studentsService.GetStudent(loggedInUser);
            var task = _tasksService.GetTask(TaskId);
            var a = _assignmentsService.GetAssignment(student.Id, TaskId);

            if (a != null)
            {
                return RedirectToAction("StudentAssignment" , new { id = a.Id });
            }else if(DateTime.Compare(DateTime.Now, task.Deadline) < 0 && a != null)
            {
                 return RedirectToAction("StudentAssignment", new { id = a.Id });
            }
            else
            {
                ViewBag.task = task.Id;
                return View();
            }
         
        }

        [HttpPost][ValidateAntiForgeryToken]
        public IActionResult Create(Guid TaskId, IFormFile file, AssignmentViewModel assignment)
        {
            string uniqueFileName = "";
            assignment.Description = HtmlEncoder.Default.Encode(assignment.Description);
            IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            string loggedInUser = User.Identity.Name;
            var student = _studentsService.GetStudent(loggedInUser);

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


                        f.CopyTo(userFile);
                        var encryptedFile = Encryption.HybridEnryption(userFile, student.PublicKey);

                        try
                        {
                            using (FileStream fsOut = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                            {
                                encryptedFile.CopyTo(fsOut);
                            }

                            f.Close();
                        }
                        catch(Exception ex)
                        {
                            
                            _logger.LogError(ex, "Error while saving the file try again later");
                            return View("Error", new ErrorViewModel() { Message = "Error while saving the file try again later" });
                        }
                       
                    }
                                                           
                    assignment.StudentId = student.Id;
                    assignment.TaskId = TaskId;
                    _assignmentsService.AddAssignment(assignment);
                    _logger.LogInformation(loggedInUser, "User Uploaded a file successfully", remoteIpAddress);
                    return Redirect("/Home/Index");                     
                }
                TempData["error"] = "File is null";
                return RedirectToAction("Create");
            }
            TempData["error"] = "Only pdf files";
            return RedirectToAction("Create");
        }

        public IActionResult ViewAssignments(string id)
        {
            string idDecrypt = Encryption.SymmetricDecrypt(id);
            Guid taskId = Guid.Parse(idDecrypt);
            var task = _tasksService.GetTask(taskId);

            if (id != null)
            {
                var list = _assignmentsService.ListAssignments(taskId);
                ViewBag.task = task;
                _logger.LogInformation("List of Assignments was show successfully");
                return View(list);
            }
            else
            {
                throw new NullReferenceException();
            } 
        }

        [AssignmentOwnerAuthorize]
        public IActionResult StudentAssignment(Guid id)
        {
            var assignment = _assignmentsService.GetAssignmentById(id);
            var task = _tasksService.GetTask(assignment.TaskId);
            var student = _studentsService.GetStudentById(assignment.StudentId);

            if (task != null && student != null)
            {
                ViewBag.task = task;
                ViewBag.student = student;
                return View(assignment);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public IActionResult Download(Guid assignmentId)
        {
            string loggedInUser = User.Identity.Name;
            var assignment = _assignmentsService.GetAssignmentById(assignmentId);

            if (User.IsInRole("STUDENT"))
            {
                string privateKey = assignment.Student.PrivateKey;
                string fileName = assignment.FileName;
                string absolutePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ValueableFiles")).Root + assignment.Path;
                try
                {
                    using (FileStream fsOut = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        MemoryStream download = Encryption.HybridDecrypt(privateKey);
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Error while saving the file try again later");
                    return View("Error", new ErrorViewModel() { Message = "Error while downloading the file try again later" });
                }

                return View();
            }
        }
    }
}
