using System;
using System.Web.Mvc;

namespace Facebook.Web.Mvc
{
    public class FacebookViewPage : ViewPage, IFacebookHttpAccessor
    {
        public FacebookViewPage()
        {
            this.Fbml = new FbmlHelper();
            this.FbIFrame = new FbIFrameHelper(this);
        }

        public FbmlHelper Fbml { get; set; }
        public FbIFrameHelper FbIFrame { get; set; }
        public FacebookHttpContext FbContext { get { return FacebookHttpContext.GetCurrent(this.Context); } }
        public FacebookHttpRequest FbRequest { get { return this.FbContext.Request; } }
        public FacebookHttpSession FbSession { get { return this.FbContext.Session; } }
    }

    public class FacebookViewPage<TModel> : ViewPage<TModel>, IFacebookHttpAccessor
        where TModel : class
    {
        public FacebookViewPage()
        {
            this.Fbml = new FbmlHelper();
            this.FbIFrame = new FbIFrameHelper(this);
        }

        public FbmlHelper Fbml { get; set; }
        public FbIFrameHelper FbIFrame { get; set; }
        public FacebookHttpContext FbContext { get { return FacebookHttpContext.GetCurrent(this.Context); } }
        public FacebookHttpRequest FbRequest { get { return this.FbContext.Request; } }
        public FacebookHttpSession FbSession { get { return this.FbContext.Session; } }
    }
}