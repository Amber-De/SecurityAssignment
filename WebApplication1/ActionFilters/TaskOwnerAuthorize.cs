
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentTask.Application.Interfaces;

namespace WebApplication1.ActionFilters
{
    public class TaskOwnerAuthorize: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                //Passing the task Id
                var taskId = new Guid(context.ActionArguments["Id"].ToString());
                var loggedInUser = context.HttpContext.User.Identity.Name;
                ITasksService tasksService = (ITasksService)context.HttpContext.RequestServices.GetService(typeof(ITasksService));

                var task = tasksService.GetTask(taskId);

                if (task.Teacher.Email != loggedInUser)
                {
                    context.Result = new UnauthorizedObjectResult("Access Denied!");
                }

                base.OnActionExecuting(context);
            }catch(Exception ex)
            {
                context.Result = new BadRequestObjectResult("bad request");
            }
        }
    }
}
