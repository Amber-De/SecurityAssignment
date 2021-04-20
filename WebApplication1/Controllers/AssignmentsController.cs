using AssignmentTask.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class AssignmentsController: Controller
    {
        private readonly IAssignmentsService _assignmentsService;;
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
            var list = _assignmentsService.GetAssignmentsList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile file)
        {
            return View();
        }


    }
}
