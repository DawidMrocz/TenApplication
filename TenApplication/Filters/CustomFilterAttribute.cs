using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace TenApplication.ActionFilters
{
    public class IdProvidedValidationAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool exist = context.ActionArguments.ContainsKey("id");
            if (context.ActionArguments["id"] != null && !exist)
            {
                context.Result = new BadRequestObjectResult("ID not found");
                return;
            }
        }
    }
}
