using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RevStackCore.Extensions.Mvc.TagHelpers
{
	[HtmlTargetElement("json-view-model")]
	public class JsonViewModelTagHelper : TagHelper
	{
		const string WINDOW_VIEWDATA = "window.__viewData";

		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			ITempDataDictionaryFactory factory = ViewContext.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
			ITempDataDictionary tempData = factory.GetTempData(ViewContext.HttpContext);

			string jsonViewModel = tempData.GetViewModel();
			string prop = tempData.GetViewModelProp();
			if (!string.IsNullOrEmpty(jsonViewModel) && !string.IsNullOrEmpty(prop))
			{
				output.TagName = "script";
				var scriptContent = new HtmlString(getScriptContent(jsonViewModel, prop));
				output.Content.SetHtmlContent(scriptContent);
			}
		}

		string getScriptContent(string jsObject, string contextProperty)
		{
			string script = "";
			if (contextProperty == null) script += WINDOW_VIEWDATA + "=" + jsObject + ";";
			else
			{
				script += WINDOW_VIEWDATA + "=" + WINDOW_VIEWDATA + " || {}; " + Environment.NewLine;
				script += WINDOW_VIEWDATA + "." + contextProperty + "=" + jsObject + ";";
			}

			return script;
		}

	}
}
