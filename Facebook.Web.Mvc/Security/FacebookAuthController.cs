using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Facebook.Api;
using Facebook.Web;
using Facebook.Web.Mvc;

namespace Facebook.Web.Security
{
    public class FacebookAuthController : FacebookMvcController
    {
        public virtual ActionResult Revoke()
        {
            this.FbContext.Auth.RevokeAuthorization(this.FbSession.Uid);
            this.FbSession.Added = false;
            this.FbContext.Session.Clear();            

            return this.Redirect("~/");
        }

        public virtual ActionResult Index(String returnUrl, ExtendedPermission? required, ExtendedPermission? optional, DeclinedPermissionsCookieDuration? duration)
        {
            this.ViewData["returnUrl"] = returnUrl ?? FormsAuthentication.DefaultUrl;

            if (required.HasValue && required != default(ExtendedPermission))
            {
                this.ViewData["required"] = required.Value.ToFieldNames().GetDelimitedString(",");
            }
            if (optional.HasValue && optional != default(ExtendedPermission))
            {
                this.ViewData["optional"] = optional.Value.ToFieldNames().GetDelimitedString(",");
            }
            if (duration.HasValue)
            {
                this.ViewData["duration"] = duration;
            }

            return this.View();
        }

        public virtual ActionResult ExtendedPermissionWarning(Boolean isAuthorized, String required, String optional)
        {
            var requiredPermissions = default(ExtendedPermission);
            var optionalPermissions = default(ExtendedPermission);
            if (!String.IsNullOrEmpty(required)) requiredPermissions = FieldName.FieldsToValues<ExtendedPermission>(required.Split(','));
            if (!String.IsNullOrEmpty(optional)) optionalPermissions = FieldName.FieldsToValues<ExtendedPermission>(optional.Split(','));
            var data = new ExtendedPermissionWarningData { RequiresAuthorization = !isAuthorized, Required = requiredPermissions, Optional = optionalPermissions };
            return this.PartialView(data);
        }

        [FacebookAuthorize]
        public virtual ActionResult SetExtendedPermissions(ExtendedPermission? declinedOptional, DeclinedPermissionsCookieDuration? duration)
        {
            if (!declinedOptional.HasValue)
            {
                if (!String.IsNullOrEmpty(Request.Form["declinedOptional"]))
                {
                    try { declinedOptional = FieldName.FieldsToValues<ExtendedPermission>(Request.Form["declinedOptional"].Split(',')); }
                    catch { }
                }
            }

            if (declinedOptional.HasValue)
            {
                this.HttpContext.SetDeclinedPermissions(this.FbContext.ApiKey, this.FbContext.Session.Uid, declinedOptional.Value, (duration ?? DeclinedPermissionsCookie.DefaultDuration));
            }

            return Content(String.Empty);
        }

        public virtual ActionResult SetConnectSession([Bind] SessionInfo session)
        {
            return null;
        }
    }
}