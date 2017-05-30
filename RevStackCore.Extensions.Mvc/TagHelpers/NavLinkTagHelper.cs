using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace RevStackCore.Extensions.Mvc.TagHelpers
{
	[HtmlTargetElement("nav-link")]
	public class NavLinkTagHelper : TagHelper
	{
		public string Controller { get; set; }
		public string Action { get; set; }
		public string Id { get; set; }
		public string Label { get; set; }
        public string Query { get; set; }

		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public IUrlHelperFactory _UrlHelper { get; set; }

		public NavLinkTagHelper(IUrlHelperFactory urlHelper)
		{
			_UrlHelper = urlHelper;
		}
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{

			var currentAttributes = context.AllAttributes;
			var cssClass = currentAttributes.FirstOrDefault(x => x.Name == "class");
			var urlHelper = _UrlHelper.GetUrlHelper(ViewContext);
			string menuUrl = "/" + Controller + "/" + Action;
			if (!string.IsNullOrEmpty(Id))
				menuUrl += "/" + Id;

            if (!string.IsNullOrEmpty(Query))
                menuUrl += '?' + Query;

			output.TagName = "a";
			output.Attributes.SetAttribute("href", $"{menuUrl}");
			output.Attributes.SetAttribute("title", Label);
			output.Content.SetContent(Label);
			var routeData = ViewContext.RouteData.Values;
			var currentController = routeData["controller"];
			var currentAction = routeData["action"];
			var currentId = routeData["id"];
			bool isActive = isActiveRoute(currentController, currentAction, currentId);
			if (isActive)
			{
				string activeCss = cssClass == null ? "active" : cssClass.Value + " active";
				output.Attributes.Add("class", activeCss);
			}
			else if (!string.IsNullOrEmpty(Id))
			{
				if (hasNavIdentifierFromView())
				{
					string activeCss = cssClass == null ? "active" : cssClass.Value + " active";
					output.Attributes.Add("class", activeCss);
				}
			}
			else
			{
				if (cssClass != null)
				{
					output.Attributes.Add("class", cssClass.Value.ToString());
				}
			}

		}

		private bool isActiveRoute(object currentController, object currentAction, object currentId)
		{
			string strCurrentId = null;
			if (currentId != null)
				strCurrentId = currentId.ToString();

			if (string.IsNullOrEmpty(strCurrentId))
				return ((String.Equals(Action, currentAction as string, StringComparison.OrdinalIgnoreCase)
						 && (String.Equals(Controller, currentController as string, StringComparison.OrdinalIgnoreCase))));
			else
				return ((String.Equals(Action, currentAction as string, StringComparison.OrdinalIgnoreCase)
						 && (String.Equals(Controller, currentController as string, StringComparison.OrdinalIgnoreCase))
						 && (String.Equals(Id, strCurrentId as string, StringComparison.OrdinalIgnoreCase))));

		}

		private bool hasNavIdentifierFromView()
		{
			try
			{
				var tempData = ViewContext.TempData;
				if (tempData != null)
				{
					var navId = tempData["nav-id"];
					if (navId != null)
					{
						return (navId.ToString().ToLower() == Id.ToLower());
					}
					else
						return false;
				}
				else
					return false;
			}
			catch (Exception)
			{
				return false;
			}

		}
	}
}
