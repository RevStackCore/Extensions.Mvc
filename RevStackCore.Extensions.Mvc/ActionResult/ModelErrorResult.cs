using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RevStackCore.Extensions.Mvc
{
    public class ModelErrorResult : IActionResult
    {
        private int _statusCode;
        private ModelStateDictionary _modelState;
		public ModelErrorResult(ModelStateDictionary modelState)
		{
            _modelState = modelState;
            _statusCode = StatusCodes.Status400BadRequest;
		}
		public ModelErrorResult(ModelStateDictionary modelState, int statusCode)
		{
            _modelState = modelState;
			_statusCode = statusCode;
		}
        public Task ExecuteResultAsync(ActionContext context)
        {
            var validationResult = new ValidationResultModel(_modelState);
            var result = new ObjectResult(validationResult);
            result.StatusCode = _statusCode;
			return Task.FromResult(result);
        }
    }
}
