using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Facebook.Web.Mvc;
using System.Web.Routing;

namespace Facebook.Api.DemoSite.Controllers
{
    [Authorize]
    public partial class DemoController : FacebookMvcController
    {
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PostToDemo(String section, String demo)
        {
            var actionName = String.Format("{0}{1}{2}{3}",
                section.Substring(0, 1).ToUpper(),
                section.Substring(1).ToLower(),
                demo.Substring(0, 1).ToUpper(),
                demo.Substring(1).ToLower());

            this.ActionInvoker.InvokeAction(this.ControllerContext, actionName);
            return this.PostResult;
        }

        private ActionResult PostResult { get; set; }
    }
}