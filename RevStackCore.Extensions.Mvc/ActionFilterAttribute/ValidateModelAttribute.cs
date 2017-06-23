using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RevStackCore.Extensions.Mvc
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
			if (!context.ModelState.IsValid)
			{
                var value = new ValidationResultModel(context.ModelState);
                context.Result = new BadRequestObjectResult(value);
			}
        }
    }
}
