using System;
using System.Web.Mvc;

namespace Facebook.Api.DemoSite.Models
{
    public class FbRedirectResult : ActionResult
    {
        public FbRedirectResult(String url)
        {
            this.Url = url;
        }

        public String Url { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write("<fb:redirect url=\"");
            context.HttpContext.Response.Write(this.Url);
            context.HttpContext.Response.Write("\" />");
        }
    }
}