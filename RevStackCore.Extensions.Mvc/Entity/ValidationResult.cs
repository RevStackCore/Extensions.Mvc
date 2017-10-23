using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RevStackCore.Extensions.Mvc
{
    public class ValidationResultModel
    {
		public string Message { get; }

		public List<ValidationError> Errors { get; }

		public ValidationResultModel(ModelStateDictionary modelState)
        {
			Errors = modelState.Keys
					.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
					.ToList();
            Message = Errors.FirstOrDefault().Message;
		}
        public ValidationResultModel(string error)
        {
            Message = error;
            Errors = new List<ValidationError>();
        }
    }
}
