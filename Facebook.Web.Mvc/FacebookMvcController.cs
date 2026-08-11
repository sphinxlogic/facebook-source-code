using System;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Web.Mvc
{
    public class FacebookMvcController : Controller
    {
        public FacebookHttpContext FbContext { get { return FacebookHttpContext.GetCurrent(this.HttpContext.Items); } }
        public FacebookHttpRequest FbRequest { get { return this.FbContext.Request; } }
        public FacebookHttpSession FbSession { get { return this.FbContext.Session; } }

        protected void AppendFbDomResourceId()
        {
            this.ViewData["FbDomResourceId"] = String.Format("{0}_result_{1}", this.ControllerContext.RouteData.Values["action"], DateTime.UtcNow.Ticks);
        }
    }
}