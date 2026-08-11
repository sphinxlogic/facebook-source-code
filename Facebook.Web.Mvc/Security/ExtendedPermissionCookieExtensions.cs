using System;
using System.Web;

namespace Facebook.Web.Security
{
    public static class DeclinedPermissionsCookieExtensions
    {
        public static ExtendedPermission GetDeclinedPermissions(this HttpContextBase httpContext, String apiKey, Int64 uid)
        {
            return DeclinedPermissionsCookie.GetDeclinedPermissions(apiKey, uid, httpContext.Request.Cookies);
        }

        public static void SetDeclinedPermissions(this HttpContextBase httpContext, String apiKey, Int64 uid, ExtendedPermission declinedPermissions, DeclinedPermissionsCookieDuration duration)
        {
            var cookie = new DeclinedPermissionsCookie(apiKey, uid, declinedPermissions, duration);
            cookie.SetCookie(httpContext.Request.Cookies, httpContext.Response.Cookies);
        }
    }
}