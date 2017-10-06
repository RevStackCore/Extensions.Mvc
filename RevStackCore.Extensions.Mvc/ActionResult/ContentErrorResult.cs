using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RevStackCore.Extensions.Mvc
{
    public class ContentErrorResult : IActionResult
    {
		private int _statusCode;
        private string _errorMessage;
		public ContentErrorResult(string errorMessage)
		{
            _errorMessage = errorMessage;
			_statusCode = StatusCodes.Status400BadRequest;
		}
		public ContentErrorResult(string errorMessage, int statusCode)
		{
			_errorMessage = errorMessage;
			_statusCode = statusCode;
		}
		public Task ExecuteResultAsync(ActionContext context)
		{
            var validationResult = new ValidationResultModel(_errorMessage);
			var result = new ObjectResult(validationResult);
			result.StatusCode = _statusCode;
			return Task.FromResult(result);
		}
    }
}
