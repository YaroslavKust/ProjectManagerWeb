using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManager.DAL.UnitOfWorks;
using ProjectManager.Entities.Models;

namespace ProjectManager.API.ActionFilters
{
    public class ValidateProjectExistsAttribute : IAsyncActionFilter
    {
        private IUnitOfWork _unit;

        public ValidateProjectExistsAttribute(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = Int32.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var projectId = (int)context.ActionArguments["projectId"];
            var project = await _unit.Projects.GetProjectAsync(userId, projectId);

            if (project is null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
            
        }
    }
}
