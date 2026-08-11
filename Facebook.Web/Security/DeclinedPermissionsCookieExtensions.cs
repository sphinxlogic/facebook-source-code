using System;
using System.Web;

namespace Facebook.Web.Security
{
    public static class DeclinedPermissionsCookieExtensions
    {
        public static ExtendedPermission GetDeclinedPermissions(this HttpRequest request, String apiKey, Int64 uid)
        {
            return DeclinedPermissionsCookie.GetDeclinedPermissions(apiKey, uid, request.Cookies);
        }
    }
}