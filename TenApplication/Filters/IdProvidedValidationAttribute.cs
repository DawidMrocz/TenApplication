using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace TenApplication.ActionFilters
{
    public class IdProvidedValidationAttribute : ActionFilterAttribute, IActionFilter
    {
        public required string IdName { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments[IdName] is null)
            {
                context.Result = new BadRequestObjectResult("ID not found");
                return;
            }
        }
    }
}
