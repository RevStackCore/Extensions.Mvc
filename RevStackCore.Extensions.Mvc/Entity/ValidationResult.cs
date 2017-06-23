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
            Message = "Error: " + modelState.Keys.SelectMany(x => modelState[x].Errors.FirstOrDefault().ErrorMessage);
			Errors = modelState.Keys
					.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
					.ToList();
		}
        public ValidationResultModel(string error)
        {
            Message = error;
            Errors = new List<ValidationError>();
        }
    }
}
