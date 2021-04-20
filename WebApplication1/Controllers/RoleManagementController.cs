using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //ALLOCATING AND DEALLOCATING ROLES
    [Authorize(Roles = "TEACHER")]
    public class RoleManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
     
        public RoleManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            RolesManagementModel model = new RolesManagementModel();
            model.Users = _userManager.Users.ToList();
            model.Roles = _roleManager.Roles.ToList();
            return View(model);
        }
        public async Task<IActionResult> AllocateRoleAsync(string role, string user, string btnName)
        {
            var returnedUser = await _userManager.FindByNameAsync(user);

            if (btnName == "Allocate")
            {
               
                if (returnedUser != null)
                {
                    await _userManager.AddToRoleAsync(returnedUser, role);
                    TempData["message"] = "successfully allocated";
                }
                else
                {
                    TempData["error"] = "user not found";
                }
            }
            else
            {
                if (returnedUser != null)
                {
                    await _userManager.AddToRoleAsync(returnedUser, role);
                    TempData["message"] = "successfully deallocated";
                }
                else
                {
                    TempData["error"] = "user not found";
                }
            }
            return RedirectToAction("Index");
        }
    }
}
