using System;
using System.Linq;
using System.Web;
using System.Web.Handlers;
using System.Web.Security;
using Facebook.Web.Configuration;

namespace Facebook.Web
{
    public class FacebookHttpModule : IHttpModule
    {
        internal static String CONTEXT_ITEMS_KEY = "__FACEBOOK_HTTP_MODULE";
        internal static readonly Type[] IgnoredHandlerTypes = new Type[]
        {
            typeof(DefaultHttpHandler),
            typeof(AssemblyResourceLoader)
        };

        /// <summary>Occurs when the <see cref="FacebookHttpContext" /> is first initialized for a user session.</summary>
        /// <remarks><para>Note: <see cref="FacebookHttpContext" /> objects are serialized and re-used between requests for each user session,
        /// so initialization should only occur once when the session starts.</para></remarks>
        public event FacebookWebEventHandler FacebookContextInit;

        /// <summary>Sets the handlers for the <see cref="HttpApplication.AcquireRequestState" /> and <see cref="FacebookContextInit" /> events.</summary>
        public virtual void Init(HttpApplication application)
        {
            this.Application = application;
            application.AcquireRequestState += new EventHandler(FacebookApplication_AcquireRequestState);
        }

        /// <summary>Gets a reference to the <see cref="HttpApplication" /> that initialized this module.</summary>
        public HttpApplication Application { get; private set; }

        /// <summary>Causes initialization of all Facebook web-specific objects and forces Forms-based authentication based on the Facebook session.</summary>
        /// <remarks>
        /// <para>Initalizes the <see cref="FacebookHttpRequest" />, <see cref="FacebookHttpSession" /> and <see cref="FacebookHttpContext" /> objects for the current request.</para>
        /// <para>If an authenticated Facebook session is detected, the ASP.NET session is marked as authenticated using <see cref="FormsAuthentication.SetAuthCookie(String, Boolean)" />.
        /// The <see cref="String" /> form of the user's Facebook UID is passed as the user name.</para>
        /// </remarks>
        void FacebookApplication_AcquireRequestState(Object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext httpContext = application.Context;
            httpContext.Response.AddHeader("P3P", "CP=\"CAO PSA OUR\"");

            httpContext.Items[FacebookHttpModule.CONTEXT_ITEMS_KEY] = this;

            Type handlerType = null;
            if (httpContext.Handler != null) handlerType = httpContext.Handler.GetType();
            
            if (handlerType != null && !IgnoredHandlerTypes.Any(h => handlerType == h))
            {
                var context = FacebookHttpContext.InitRequest(httpContext);

                if (FacebookWebSection.IsConfigured &&
                    FacebookWebSection.Current.UseFormsAuthentication &&
                    context.HasSession)
                {
                    FormsAuthentication.SetAuthCookie(context.Session.Uid.ToString(), context.Session.Expires == DateTime.MaxValue);
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
            }
        }

        /// <summary>Raises the <see cref="FacebookContextInit" /> event of the <see cref="FacebookHttpApplication" /> object.</summary>
        /// <param name="e">A <see cref="FacebookWebEventArgs" /> object that contains data about the event.</param>
        protected internal void OnFacebookContextInit(FacebookWebEventArgs e)
        {
            if (this.FacebookContextInit != null)
            {
                this.FacebookContextInit(this, e);
            }
        }

        void IHttpModule.Dispose() { }
    }
}
