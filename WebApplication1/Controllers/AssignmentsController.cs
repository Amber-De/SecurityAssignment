using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class AssignmentsController: Controller
    {
        private readonly IAssignmentsService _assignmentsService;
        private readonly IWebHostEnvironment _host;
        private readonly ILogger _logger;

        public AssignmentsController(IAssignmentsService assignmentsService, IWebHostEnvironment host, ILogger<AssignmentsController> logger)
        {
            _assignmentsService = assignmentsService;
            _host = host;
            _logger = logger;
        }

        public IActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile file, AssignmentViewModel assignment)
        {
            string uniqueFileName = "";

            if (Path.GetExtension(file.FileName) == ".pdf")
            {
                byte[] whitelist = new byte[] { 37, 80, 68, 70 };

                if (file != null)
                {
                    using (var f = file.OpenReadStream())
                    {
                        //Reading a number of bytes at the same type(4) and not skipping any(0)
                        byte[] buffer = new byte[4];
                        f.Read(buffer, 0, 4);

                        for(int i = 0; i < whitelist.Length; i++)
                        {
                            if(whitelist[i] != buffer[i])
                            {
                                ModelState.AddModelError("file", "File is not valid nor accepted");
                                return View();
                            }
                        }
                        f.Position = 0;

                        uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        assignment.Path = uniqueFileName;
                        string absolutePath = _host.ContentRootPath + @"\Files\" + uniqueFileName;

                        using (FileStream fsOut = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            f.CopyTo(fsOut);
                        }
                          
                        f.Close();
                    }

                    assignment.Student.Name = HttpContext.User.Identity.Name;
                    _assignmentsService.AddAssignment(assignment);
                }
            }

            return View();
        }


    }
}
