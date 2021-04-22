using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AssignmentTask.Application.AutoMapper;
using AssignmentTask.Data.Context;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.Services;
using AssignmentTask.Data.Repositories;

namespace AssignmentTask.IOC
{
   public class DependencyContainer
   { 
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {

            services.AddDbContext<TaskDbContext>(options =>
            {
              options.UseSqlServer(
               connectionString);//.UseLazyLoadingProxies();
            });

            services.AddScoped<ITeachersRepository, TeachersRepository>();
            services.AddScoped<ITeachersService, TeachersService>();

            services.AddScoped<IStudentsRepository, StudentsRepository>();
            services.AddScoped<IStudentsService, StudentsService>();

            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ITasksService, TasksService>();

            services.AddScoped<IAssignmentsRepository, AssignmentsRepository>();
            services.AddScoped<IAssignmentsService, AssignmentsService>();

            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            AutoMapperConfiguration.RegisterMappings();

        }
   }
}
