using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RevStackCore.Extensions.Mvc.TagHelpers
{
    [HtmlTargetElement("form-message")]
    public class FormMessageTagHelper : TagHelper
    {
        public string CssClass { get; set; }
        public FormMessageModel Model { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string defaultCss = "form-message-container";
            string defaultId = "formMessageContainer";
            string semanticCss = getSemanticCss(Model.MessageType);
            string semanticIcon = getSemanticIcon(Model.MessageType);
            if (!string.IsNullOrEmpty(CssClass))
                defaultCss += " " + CssClass;

            string linkClass = "hide";
            if (!string.IsNullOrEmpty(Model.Link))
            {
                linkClass = "";

            }
            output.TagName = "div";
            output.Attributes.SetAttribute("class", defaultCss);
            output.Attributes.SetAttribute("id", defaultId);
            output.Content.SetHtmlContent(
                $@"<div class='form-message {semanticCss}'  id='formMessage'>
                     <div class='icon-content-close' id='formMessageClose'></div>
                     <div class='status' id='formMessageStatus'>
                         <span class='{semanticIcon}' id='formStatusIcon'></span>
                     </div>
                     <div class='status-description' id='formMessageDescription'>{Model.Message}</div>
                     <a href='{Model.Link}' class='{linkClass}' id='formMessageLink'>{Model.LinkLabel}</a>
                   </div>
                 "
            );

            output.TagMode = TagMode.StartTagAndEndTag;

        }

        private string getSemanticCss(FormMessageType type)
        {
            if (type == FormMessageType.Error)
            {
                return "semantic-error";
            }
            else if (type == FormMessageType.Success)
            {
                return "semantic-success";
            }
            else if (type == FormMessageType.Info)
            {
                return "semantic-info";
            }
            else if (type == FormMessageType.Warning)
            {
                return "semantic-warning";
            }
            else
            {
                return "";
            }
        }

        private string getSemanticIcon(FormMessageType type)
        {
            if (type == FormMessageType.Error)
            {
                return "icon-content-error";
            }
            else if (type == FormMessageType.Success)
            {
                return "icon-content-check";
            }
            else if (type == FormMessageType.Info)
            {
                return "icon-content-info";
            }
            else if (type == FormMessageType.Warning)
            {
                return "icon-content-warning";
            }
            else
            {
                return "";
            }
        }
    }
}
