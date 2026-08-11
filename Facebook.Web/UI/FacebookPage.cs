using System;
using System.Web.UI;

namespace Facebook.Web.UI
{
    public class FacebookPage : Page
    {
        public FacebookHttpContext FbContext { get { return FacebookHttpContext.GetCurrent(this.Context); } }
        public FacebookHttpRequest FbRequest { get { return this.FbContext.Request; } }
        public FacebookHttpSession FbSession { get { return this.FbContext.Session; } }
    }
}