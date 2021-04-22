using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeachersService _teachersService;
        public TeachersController(ITeachersService teachersService)
        {
            _teachersService = teachersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(StudentViewModel student)
        {
            _teachersService.AddStudent(student);         
            return View();
        }
    }
}
