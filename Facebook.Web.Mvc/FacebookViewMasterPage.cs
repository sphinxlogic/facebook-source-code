using System;
using System.Web.Mvc;

namespace Facebook.Web.Mvc
{
    public class FacebookViewMasterPage : ViewMasterPage, IFacebookHttpAccessor
    {
        public FacebookViewMasterPage()
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

    public class FacebookViewMasterPage<TModel> : ViewMasterPage<TModel>, IFacebookHttpAccessor
        where TModel : class
    {
        public FacebookViewMasterPage()
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