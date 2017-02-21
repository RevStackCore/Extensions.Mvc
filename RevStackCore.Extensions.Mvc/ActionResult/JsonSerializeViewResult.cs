using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace RevStackCore.Extensions.Mvc
{
	public class JsonSerializeViewResult : ViewResult
	{
		private ViewResult _innerResult;
		private string _jsonModel;
		private string _contextProp;
		public JsonSerializeViewResult(ViewResult innerResult, string jsonModel, string contextProp)
		{
			_innerResult = innerResult;
			_jsonModel = jsonModel;
			_contextProp = contextProp;
		}

		public override void ExecuteResult(ActionContext context)
		{
			ITempDataDictionaryFactory factory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
			ITempDataDictionary tempData = factory.GetTempData(context.HttpContext);
			tempData.SetViewModel(_jsonModel);
			tempData.SetViewModelProp(_contextProp);
			_innerResult.ExecuteResult(context);
		}
	}
}
