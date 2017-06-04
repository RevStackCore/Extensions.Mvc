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
		public string NavId { get; set; }
		public bool FullMatch { get; set; }

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
			//default isActive route check
			bool isActive = isActiveRoute(currentController, currentAction, currentId);
			//override default active check if NavId attribute is set
			if (!string.IsNullOrEmpty(NavId))
			{
				if (isNavIdentifierMatch())
				{
					string activeCss = cssClass == null ? "active" : cssClass.Value + " active";
					output.Attributes.Add("class", activeCss);
				}
			}
			else if (isActive)
			{
				string activeCss = cssClass == null ? "active" : cssClass.Value + " active";
				output.Attributes.Add("class", activeCss);
			}
			else
			{
				if (cssClass != null)
				{
					output.Attributes.Add("class", cssClass.Value.ToString());
				}
			}

		}
		/// <summary>
		/// Checks if an active route.
		/// </summary>
		/// <returns><c>true</c>, if active route check passes, <c>false</c> otherwise.</returns>
		/// <param name="currentController">Current controller.</param>
		/// <param name="currentAction">Current action.</param>
		/// <param name="currentId">Current identifier.</param>
		private bool isActiveRoute(object currentController, object currentAction, object currentId)
		{

			//if FullMatch attribute is set, the full pattern<Controller,Action,Id> must match
			if (FullMatch)
			{
				return fullActiveRouteCheck(currentController, currentAction, currentId);
			}
			else
			{
				//default match is controller only
				return (String.Equals(Controller, currentController as string, StringComparison.OrdinalIgnoreCase));
			}
		}

        /// <summary>
        /// Checks the full route pattern for a match.
        /// </summary>
        /// <returns><c>true</c>, if active route check passes, <c>false</c> otherwise.</returns>
        /// <param name="currentController">Current controller.</param>
        /// <param name="currentAction">Current action.</param>
        /// <param name="currentId">Current identifier.</param>
		private bool fullActiveRouteCheck(object currentController, object currentAction, object currentId)
		{
			string strCurrentId = null;
			string strCurrentAction = null;
			string action = Action;
			if (currentId != null)
				strCurrentId = currentId.ToString();
			if (currentAction != null)
				strCurrentAction = currentAction.ToString();

			if (string.IsNullOrEmpty(strCurrentId) && (string.IsNullOrEmpty(strCurrentAction) || strCurrentAction == "Index"))
				return (String.Equals(Controller, currentController as string, StringComparison.OrdinalIgnoreCase));

			else if (string.IsNullOrEmpty(strCurrentId))
				return ((String.Equals(Action, currentAction as string, StringComparison.OrdinalIgnoreCase)
						 && (String.Equals(Controller, currentController as string, StringComparison.OrdinalIgnoreCase))));
			else
				return ((String.Equals(Action, currentAction as string, StringComparison.OrdinalIgnoreCase)
						 && (String.Equals(Controller, currentController as string, StringComparison.OrdinalIgnoreCase))
						 && (String.Equals(Id, strCurrentId as string, StringComparison.OrdinalIgnoreCase))));
		}

        /// <summary>
        /// Checks if NavId attribute matches the value in TempData
        /// </summary>
        /// <returns><c>true</c>, if nav identifier match was ised, <c>false</c> otherwise.</returns>
		private bool isNavIdentifierMatch()
		{
			try
			{
				var tempData = ViewContext.TempData;
				if (tempData != null)
				{
					var navId = tempData["nav-id"];
					if (navId != null)
					{
						return (navId.ToString().ToLower() == NavId.ToLower());
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
