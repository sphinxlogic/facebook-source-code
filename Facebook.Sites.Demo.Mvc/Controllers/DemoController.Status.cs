using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Facebook.Web;
using Facebook.Api.DemoSite.Models.Demo;
using Facebook.Api.DemoSite.Models;
using Facebook.Web.Mvc;

namespace Facebook.Api.DemoSite.Controllers
{
    public partial class DemoController : FacebookMvcController
    {
        [Authorize]
        public ActionResult Status()
        {
            FacebookResponse<FacebookList<UserStatus>> status = this.FbContext.Status.Get(1);
            FacebookResponse<FacebookList<UserStatus>> statusByUid = this.FbContext.Status.GetByUid(this.FbContext.Session.Uid,1);

            StatusIndexData data = new StatusIndexData();

            if (status.IsError)
            {
                data.StatusMessageFromGet = status.ResponseException.Message;
            }
            else if (status.Value.Count < 1)
            {
                data.StatusMessageFromGet = "No status found";
            }
            if (statusByUid.IsError)
            {
                data.StatusMessageFromGetByUid = statusByUid.ResponseException.Message;
            }
            else if(statusByUid.Value.Count < 1)
            {
                data.StatusMessageFromGetByUid = "No status found";
            }

            return View(data);

        }

        public ActionResult SetStatus()
        {
            if (!this.FbContext.HasSession)
            {
                return new FbRedirectResult(this.FbContext.GetAuthorizationUrl("http://apps.facebook.com/dotnetapi/demo/users"));
            }
            String status = this.Request.Params["status"];
            var result = this.FbContext.Status.Set(this.FbContext.Session.Uid, status);
            if (this.FbContext.Status.Set(this.FbContext.Session.Uid, status))
            {
                return new FbRedirectResult("/dotnetapi/demo/status");
            }
            return new FbRedirectResult("/dotnetapi/demo/status");
        }
    }
}