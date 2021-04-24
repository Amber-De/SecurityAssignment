using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AssignmentTask.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WebApplication1.Email;
using WebApplication1.Models;

namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ITeachersService _teachersService;
        private readonly IStudentsService _studentsService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, ITeachersService teachersService, IStudentsService studentsService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _teachersService = teachersService;
            _studentsService = studentsService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            public string Password { get; set; }
        }

       
        private static string CreateRandomPassword(int length = 12)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "A?BCabc!01DEde23fHJmnoKL9@MNlpOP^&QR56SqrsTU$%VjkWXYZghFGiz4tuv#w7xy8*";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            
            string pass =  new string(chars);
            
            return pass;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //user = asp.net user roles
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, Address = Input.Address};
                var result = await _userManager.CreateAsync(user, Input.Password = CreateRandomPassword());
                await _userManager.AddToRoleAsync(user, "STUDENT");
               
                var teacher = _teachersService.GetTeacherId(User.Identity.Name);
                _studentsService.AddStudent(

                    new AssignmentTask.Application.ViewModels.StudentViewModel
                    {
                        Email = Input.Email,
                        Name = Input.FirstName,
                        Surname = Input.LastName,
                        TeacherID = teacher.Id

                    }); ;

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    EmailHelper emailHelper = new EmailHelper();
                    string studentPassword = Input.Password;
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink, studentPassword);


                    if (emailResponse)
                        return RedirectToAction("Home/Index");
                    else
                    {
                        // log email failed 
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
