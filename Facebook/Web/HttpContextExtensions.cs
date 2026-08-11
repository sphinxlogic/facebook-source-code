using System;
using System.Web;

namespace Facebook.Web
{
    public static class HttpContextExtensions
    {
        public static Boolean HasFacebookRequest(this HttpContext httpContext)
        {
            return httpContext.Items[FacebookHttpRequest.KEY_FBREQUEST] != null
                || httpContext.Session[FacebookHttpRequest.KEY_FBREQUEST] != null
                || httpContext.Request.Cookies[FacebookHttpRequest.KEY_FBREQUEST] != null;
        }

        public static Boolean HasFacebookContext(this HttpContext httpContext)
        {
            return httpContext.Items.Contains(FacebookHttpContext.CONTEXT_ITEMS_KEY);
        }
    }
}