using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using Facebook.Web.Mvc;

namespace Facebook.Api.DemoSite.Controllers
{
    public class AuthController : FacebookMvcController
    {
        public ActionResult Revoke()
        {
            this.FbContext.Auth.RevokeAuthorization(this.FbSession.Uid);
            this.FbSession.Added = false;
            this.FbContext.Session.Expires = DateTime.Now;

            return this.Redirect("~/");
        }

        public ActionResult Login(String returnUrl)
        {
            this.ViewData["returnUrl"] = returnUrl ?? FormsAuthentication.DefaultUrl;
            return this.View();
        }
    }
}