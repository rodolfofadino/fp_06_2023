using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace fiap.api.ActionFilters
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.HttpContext.Request.Headers["x-api-key"].Count == 0
                ||
                context.HttpContext.Request.Headers["x-api-key"].FirstOrDefault() != "token123")
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
