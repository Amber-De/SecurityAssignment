using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AssignmentTask.Application.AutoMapper;
using AssignmentTask.Data.Context;

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


            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            AutoMapperConfiguration.RegisterMappings();

        }
   }
}
