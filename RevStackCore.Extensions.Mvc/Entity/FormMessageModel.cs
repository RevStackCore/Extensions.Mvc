using System;
namespace RevStackCore.Extensions.Mvc
{
    public class FormMessageModel
    {
        public FormMessageType MessageType { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public string LinkLabel { get; set; }
        public string CssVisible { get; set; }

        public FormMessageModel()
        {
            Message = "";
            Link = "";
            LinkLabel = "";
            CssVisible = "";
        }
    }       
}
