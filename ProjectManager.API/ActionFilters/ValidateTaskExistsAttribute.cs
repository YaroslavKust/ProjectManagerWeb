using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManager.DAL.UnitOfWorks;
using System.Threading.Tasks;

namespace ProjectManager.API.ActionFilters
{
    public class ValidateTaskExistsAttribute: IAsyncActionFilter
    {
        private IUnitOfWork _unit;

        public ValidateTaskExistsAttribute(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var projectId = (int)context.ActionArguments["projectId"];
            var id = (int)context.ActionArguments["id"];

            var task = await _unit.Tasks.GetTaskAsync(projectId, id);

            if (task is null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("SelectedTask", task);
                await next();
            }

            
        }
    }
}