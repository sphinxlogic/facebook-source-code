using System;
using System.Linq;
using System.Web;

namespace Facebook.Web.Security
{
    public class DeclinedPermissionsCookie
    {
        internal const String CookieKeyFormat = "__DeclinedPermissions__{0}__{1}__{2}";
        public const DeclinedPermissionsCookieDuration DefaultDuration = DeclinedPermissionsCookieDuration.Week;

        public DeclinedPermissionsCookie(String apiKey, Int64 uid, ExtendedPermission declinedPermissions, DeclinedPermissionsCookieDuration duration)
        {
            this.ApiKey = apiKey;
            this.Uid = uid;
            this.DeclinedPermissions = declinedPermissions;
            this.Duration = duration;
        }

        public DeclinedPermissionsCookie(String apiKey, Int64 uid, ExtendedPermission declinedPermissions)
            : this(apiKey, uid, declinedPermissions, DefaultDuration) { }
        
        public ExtendedPermission DeclinedPermissions { get; private set; }
        private String ApiKey { get; set; }
        private Int64 Uid { get; set; }
        private DeclinedPermissionsCookieDuration Duration { get; set; }

        private String GetKey(ExtendedPermission permission)
        {
            return GetKey(this.ApiKey, this.Uid, permission);
        }

        private static String GetKey(String apiKey, Int64 uid, ExtendedPermission permission)
        {
            return String.Format(CookieKeyFormat, apiKey, uid, ((Int64)permission).ToString());
        }

        public void SetCookie(HttpCookieCollection requestCookies, HttpCookieCollection responseCookies)
        {
            foreach (var permission in Enum.GetValues(typeof(ExtendedPermission)).OfType<ExtendedPermission>().Where(v => (this.DeclinedPermissions & v) == v))
            {
                var key = this.GetKey(permission);
                var existingCookie = requestCookies[key];
                if (existingCookie == null)
                {
                    var cookie = new HttpCookie(key, permission.ToString());
                    if (this.Duration != DeclinedPermissionsCookieDuration.Session)
                    {
                        cookie.Expires = DateTime.UtcNow.AddTicks((Int64)this.Duration);
                    }
                    responseCookies.Add(cookie);
                }
            }
        }

        public static ExtendedPermission GetDeclinedPermissions(String apiKey, Int64 uid, HttpCookieCollection cookies)
        {
            var declinedPermissions = default(ExtendedPermission);
            var relevantCookieKeys = cookies.AllKeys.Where(key => key.StartsWith(String.Format(CookieKeyFormat, apiKey, uid, String.Empty)));
            foreach (var cookieKey in relevantCookieKeys)
            {
                var cookie = cookies[cookieKey];
                var value = default(ExtendedPermission);
                try { value = (ExtendedPermission)Enum.Parse(typeof(ExtendedPermission), cookie.Value); }
                finally { declinedPermissions |= value; }                
            }

            return declinedPermissions;
        }
    }
}