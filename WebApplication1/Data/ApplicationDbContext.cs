using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using AssignmentTask.Application.ViewModels;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AssignmentTask.Application.ViewModels.TaskViewModel> TaskViewModel { get; set; }
        public DbSet<AssignmentTask.Application.ViewModels.AssignmentViewModel> AssignmentViewModel { get; set; }
        public DbSet<AssignmentTask.Application.ViewModels.CommentViewModel> CommentViewModel { get; set; }
    }
}
