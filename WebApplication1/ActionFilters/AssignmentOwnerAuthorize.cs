using AssignmentTask.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;


namespace WebApplication1.ActionFilters
{
    public class AssignmentOwnerAuthorize: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                //Passing the assignment Id
                var Id = new Guid(context.ActionArguments["id"].ToString());
                var loggedInUser = context.HttpContext.User.Identity.Name;
                IAssignmentsService assignmentsService = (IAssignmentsService)context.HttpContext.RequestServices.GetService(typeof(IAssignmentsService));

                var assignment = assignmentsService.GetAssignmentById(Id);


                if (assignment.Student.Email == loggedInUser || assignment.Student.Teacher.Email == loggedInUser)
                {
                    base.OnActionExecuting(context);
                } else
                {
                    context.Result = new UnauthorizedObjectResult("Access Denied!");
                }
            }
            catch (Exception ex)
            {
                context.Result = new BadRequestObjectResult("bad request");
            }
        }
    }
}
