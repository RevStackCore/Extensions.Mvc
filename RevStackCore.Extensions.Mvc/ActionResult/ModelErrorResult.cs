using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RevStackCore.Extensions.Mvc
{
    public class ModelErrorResult : BadRequestObjectResult
    {

        public ModelErrorResult(ModelStateDictionary modelState) :base(modelState) 
        {
            var validationResult = new ValidationResultModel(modelState);
			Value = validationResult;
			StatusCode = StatusCodes.Status400BadRequest;
        }

		public ModelErrorResult(ModelStateDictionary modelState, int statusCode) : base(modelState)
		{
			var validationResult = new ValidationResultModel(modelState);
			Value = validationResult;
            StatusCode = statusCode;
		}

    }
}
