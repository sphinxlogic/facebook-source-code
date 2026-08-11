using System;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Facebook.Sites.Desktop.Auth.Models
{
    public class XmlResult : ActionResult
    {
        public XmlResult(XDocument content)
        {
            this.Content = content;
        }

        public XDocument Content { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (this.Content != null)
            {
                context.HttpContext.Response.ContentType = "text/xml";
                using (var stream = context.HttpContext.Response.OutputStream)
                {
                    using (var writer = XmlWriter.Create(stream))
                    {
                        this.Content.WriteTo(writer);
                    }
                }
            }            
        }
    }
}