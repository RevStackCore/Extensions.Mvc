using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RevStackCore.Extensions.Mvc.ActionResult
{
    public class ContentRedirectResult : IActionResult
    {
        private HttpResponse _response;
        private string _url;
        private int _statusCode;
        private string _header = "X-Location";

        public ContentRedirectResult(HttpResponse response, string url)
        {
            _response = response;
            _url = url;
            _statusCode = StatusCodes.Status303SeeOther;
        }

		public ContentRedirectResult(HttpResponse response, string url, int statusCode)
		{
			_response = response;
			_url = url;
            _statusCode = statusCode;
		}

		public ContentRedirectResult(HttpResponse response, string url, int statusCode, string header)
		{
			_response = response;
			_url = url;
			_statusCode = statusCode;
            _header = header;
		}

        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(new {});
            result.StatusCode = _statusCode;
            _response.Headers.Add(_header,_url);
            return Task.FromResult(result);
        }
    }
}
