using System;
using System.Web.Security;
using System.Web.Mvc;
using Facebook.Web;
using Facebook.Web.Mvc;

namespace $rootnamespace$.Controllers
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

        public ActionResult Redirect(String path)
        {
            return this.Redirect(path);
        }
    }
}