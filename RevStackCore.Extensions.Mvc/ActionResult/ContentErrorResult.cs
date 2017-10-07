using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RevStackCore.Extensions.Mvc
{
    public class ContentErrorResult : ObjectResult
	{
		public ContentErrorResult(object value) : base(value)
		{
			var validationResult = new ValidationResultModel(value.ToString());
			Value = validationResult;
			StatusCode = StatusCodes.Status400BadRequest;
		}

		public ContentErrorResult(object value, int statusCode) : base(value)
		{
			var validationResult = new ValidationResultModel(value.ToString());
			Value = validationResult;
			StatusCode = statusCode;
		}

	}
}
