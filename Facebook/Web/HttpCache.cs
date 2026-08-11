using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using Newtonsoft.Json;
using Newtonsoft.Json;

namespace Facebook.Web
{
    /// <summary>An implementation of <see cref="ICacheWrapper" /> that provides access to the ASP.NET HTTP Cache (<see cref="HttpRuntime.Cache" />).</summary>
    public class HttpCache : ICacheWrapper
    {
        public void Init(HttpContext context)
        {
            this.Cache = context.Cache;
            this.ContextItems = context.Items;
            this.Session = context.Session;
            this.RequestCookies = context.Request.Cookies;
            this.ResponseCookies = context.Response.Cookies;
        }

        #region [ ICacheWrapper Members ]

        void ICacheWrapper.Add(String key, Object value, CacheScope scope)
        {
            if (scope == CacheScope.NotCached) return;

            this.ContextItems[key] = value;
            if (scope == CacheScope.Request) return;

            TimeSpan duration = TimeSpan.MinValue;
            switch (scope)
            {
                case CacheScope.Session:
                    if (this.Session == null) throw new FacebookException("A request was made to store an item at session scope, but the session is not available.");
                    else duration = new TimeSpan(0, this.Session.Timeout, 0);
                    break;
                    
                case CacheScope.MaxDuration:
                    duration = FacebookCache.MaxDuration;
                    break;

                case CacheScope.Permanent:
                    duration = FacebookCache.PermanentDuration;
                    break;
            }

            this.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, duration, CacheItemPriority.Normal, null);
            this.Session[key] = value;
            this.AddCookie(key, value, duration);
        }

        Object ICacheWrapper.Remove(String key)
        {
            this.ContextItems.Remove(key);
            if (this.Session != null) this.Session.Remove(key);
            this.ResponseCookies.Remove(key);
            return this.Cache.Remove(key);
        }

        Boolean ICacheWrapper.ContainsKey(String key)
        {
            if (this.ContextItems.Contains(key)) return true;
            else if (this.Cache[key] != null) return true;
            else if (this.Session != null && this.Session[key] != null) return true;
            else if (this.RequestCookies[key] != null) return true;
            else return false;
        }

        Object ICacheWrapper.this[String key]
        {
            get
            {
                if (this.ContextItems.Contains(key)) return this.ContextItems[key];
                else if (this.Cache[key] != null) return this.Cache[key];
                else if (this.Session != null && this.Session[key] != null) return this.Session[key];
                else if (this.RequestCookies[key] != null) return this.GetCookie(key);
                else return null;
            }
            set { ((ICacheWrapper)this).Add(key, value, CacheScope.Session); }
        }

        #endregion

        private void AddCookie(String key, Object value, TimeSpan duration)
        {
            var cookie = new HttpCookie(key);
            cookie.Values.Add("value", JsonConvert.SerializeObject(value));
            cookie.Values.Add("type", value.GetType().FullName);
            cookie.Expires = DateTime.Now.Add(duration);
            this.ResponseCookies.Add(cookie);
        }

        private Object GetCookie(String key)
        {
            var cookie = this.RequestCookies[key];
            if (cookie == null) return null;
            else
            {
                var strValue = cookie.Values["value"];
                if (strValue == null) return null;

                var strType = cookie.Values["type"];
                if (strType == null) return null;

                var type = Type.GetType(strType);
                var value = JsonConvert.DeserializeObject(strValue, type);

                return value;
            }
        }

        private Cache Cache { get; set; }
        private IDictionary ContextItems { get; set; }
        private HttpSessionState Session { get; set; }
        private HttpCookieCollection RequestCookies { get; set; }
        private HttpCookieCollection ResponseCookies { get; set; }
    }
}