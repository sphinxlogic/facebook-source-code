using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Facebook.Api;
using Facebook.Configuration;
using Facebook.Web.Configuration;
using System.Collections;

namespace Facebook.Web
{
    /// <summary>A web-specific implementation of <see cref="FacebookContextBase{TCache, TSession}" /> that encapsulates all Facebook-specific information
    /// about an individual HTTP request for a Facebook web application.</summary>
    [Serializable]
    public partial class FacebookHttpContext
    {
        internal const String CONTEXT_ITEMS_KEY = "__FBCONTEXT";

        private FacebookHttpContext(HttpContext httpContext, String apiKey, String secret)
            : this(apiKey, secret)
        {
            this.HttpContext = httpContext;
        }

        protected internal HttpContext HttpContext { get; set; }

        /// <summary>Initializes the Facebook web objects.</summary>
        /// <param name="httpContext">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
        public static FacebookHttpContext InitRequest(HttpContext httpContext)
        {
            return FacebookHttpContext.InitRequest(httpContext, null, null);
        }

        public static FacebookHttpContext InitRequest(HttpContext httpContext, String apiKey, String secret)
        {
            FacebookHttpContext context = null;
            FacebookHttpSession session = null;
            FacebookHttpRequest request = null;

            if (String.IsNullOrEmpty(apiKey) || String.IsNullOrEmpty(secret))
            {
                var config = FacebookHttpContext.GetConfigForRequest(httpContext, session);
                apiKey = config.ApiKey;
                secret = config.Secret;
            }

            if (httpContext.HasFacebookContext()) context = FacebookHttpContext.GetCurrent(httpContext);
            else context = new FacebookHttpContext(httpContext, apiKey, secret);

            context.Cache.HttpInit(apiKey, httpContext);
            context.SessionCache.HttpInit(apiKey, httpContext);

            request = FacebookHttpRequest.Init(context);
            session = FacebookHttpSession.Init(context);

            session.MergeConnect(httpContext, apiKey);

            if (session.Expires < DateTime.Now)
            {
                session.Clear();
                session = new FacebookHttpSession(httpContext);
            }

            context.InitContext(apiKey, secret, request, session);

            if (context.HasSession &&
                (request.InCanvas || request.InIFrame || request.InProfileTab || request.Uid != 0))
            {
                if (!context.ValidateSignature(httpContext.Request.Params))
                {
                    throw new FacebookException("The signature on the request is not valid.");
                }
            }
            context.SetCurrent();
            return context;
        }

        private static FacebookApplicationElement GetConfigForRequest(HttpContext httpContext)
        {
            return GetConfigForRequest(httpContext, null);
        }

        private static FacebookApplicationElement GetConfigForRequest(HttpContext httpContext, FacebookHttpSession session)
        {
            FacebookApplicationElement config = null;
            if (FacebookSectionGroup.HasApiSection && session != null)
            {
                FacebookSectionGroup.Api.Applications.GetByApiKey(session.ApiKey);
            }
            if (config == null) config = FacebookSectionGroup.Api.Applications.GetCurrent();
            if (config == null) throw new FacebookException("An attempt was made to initialize FacebookHttpContext with the supplied api key, but no application configurations were found.");
            else return config;
        }

        private void InitContext(String apiKey, String secret, FacebookHttpRequest request, FacebookHttpSession session)
        {
            FacebookHttpContext.InitContext(this, this.HttpContext, apiKey, secret, request, session);
        }

        private static FacebookHttpContext InitContext(FacebookHttpContext context, HttpContext httpContext, FacebookHttpRequest request, FacebookHttpSession session)
        {
            return FacebookHttpContext.InitContext(context, httpContext, null, null, request, session);
        }

        private static FacebookHttpContext InitContext(FacebookHttpContext context, HttpContext httpContext, String apiKey, String secret, FacebookHttpRequest request, FacebookHttpSession session)
        {
            context.ApiKey = apiKey;
            context.Secret = secret;
            context.HttpContext = httpContext;
            context.Request = request;
            context.Session = session;
            
            if (httpContext.Items[FacebookHttpModule.CONTEXT_ITEMS_KEY] != null)
            {
                var module = (FacebookHttpModule)httpContext.Items[FacebookHttpModule.CONTEXT_ITEMS_KEY];
                module.OnFacebookContextInit(new FacebookWebEventArgs(request, session, context));
            }

            return context;
        }

        /// <summary>Gets a reference to the <see cref="FacebookHttpContext" /> object for the current HTTP request.</summary>
        public static FacebookHttpContext GetCurrent(HttpContext httpContext)
        {
            return (FacebookHttpContext)httpContext.Items[CONTEXT_ITEMS_KEY];
        }

        public static FacebookHttpContext GetCurrent(IDictionary items)
        {
            return (FacebookHttpContext)items[CONTEXT_ITEMS_KEY];
        }

        public void SetCurrent()
        {
            this.HttpContext.Items[CONTEXT_ITEMS_KEY] = this;
            this.Session.Persist();
        }

        public Boolean HasPersistedFacebookSession()
        {
            var sessionCacheKey = FacebookHttpSession.GetSessionCacheKey(this.HttpContext);
            return this.SessionCache.ContainsKey(sessionCacheKey);
        }

        /// <summary>Gets a reference to the <see cref="FacebookHttpSession" /> object for the current HTTP request.</summary>
        public override FacebookHttpSession Session { get; protected internal set; }

        /// <summary>Gets a reference to the <see cref="FacebookHttpRequest" /> object for the current HTTP request.</summary>
        public FacebookHttpRequest Request { get; protected internal set; }

        public FacebookWebApplication ApplicationInfo { get { return FacebookWebApplication.Get(this); } }

        /// <summary>Generates a url that a user can be redirected to in order to authenticate for the current Facebook application.</summary>
        /// <param name="next">An optional url to redirect to after authentication.</param>
        /// <returns>A url that a user can be redirected to in order to authenticate for the current Facebook application.</returns>
        public String GetAuthorizationUrl(String next)
        {
            return String.Format("http://www.facebook.com/login.php?v={0}&api_key={1}&next={2}&canvas=", this.Version, this.ApiKey, String.IsNullOrEmpty(next) ? String.Empty : HttpUtility.UrlEncode(next));
        }

        /// <summary>Validates the signature of a request or response sent from Facebook.</summary>
        /// <param name="requestParams">The collection of parameters sent with the request or response.</param>
        /// <returns><c>true</c> if the signature validates; otherwise, false.</returns>
        /// <remarks>See http://wiki.developers.facebook.com/index.php/Verifying_The_Signature for more information.</remarks>
        public virtual Boolean ValidateSignature(NameValueCollection requestParams)
        {
            String prefix = null;
            String sigKey = null;
            if (requestParams[this.ApiKey] != null) // connect requests
            {
                prefix = String.Format("{0}_", this.ApiKey);
                sigKey = this.ApiKey;
            }
            else // canvas requests
            {
                prefix = "fb_sig_";
                sigKey = "fb_sig";
            }
            String signature = requestParams[sigKey];
            if (String.IsNullOrEmpty(signature))
                return false;

            String s = (
                from key in requestParams.AllKeys
                where key != null && key.StartsWith(prefix)
                orderby key
                select key.Substring(prefix.Length) + "=" + requestParams[key]).Append() + this.Secret;

            StringBuilder computedSignature = new StringBuilder();
            MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(s)).ToList().ForEach(b => computedSignature.AppendFormat("{0:x2}", b));

            return computedSignature.ToString().ToLowerInvariant() == signature.ToLowerInvariant();
        }
    }
}