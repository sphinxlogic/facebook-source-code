using System;
using System.Web.Mvc;

namespace Facebook.Web.Mvc
{
    public class FacebookViewUserControl : ViewUserControl, IFacebookHttpAccessor
    {
        public FacebookViewUserControl()
        {
            this.Fbml = new FbmlHelper();
            this.FbIFrame = new FbIFrameHelper(this);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        public FbmlHelper Fbml { get; set; }
        public FbIFrameHelper FbIFrame { get; set; }
        public FacebookHttpContext FbContext { get { return FacebookHttpContext.GetCurrent(this.Context); } }
        public FacebookHttpRequest FbRequest { get { return this.FbContext.Request; } }
        public FacebookHttpSession FbSession { get { return this.FbContext.Session; } }
    }

    public class FacebookViewUserControl<TModel> : ViewUserControl<TModel>, IFacebookHttpAccessor
        where TModel : class
    {
        public FacebookViewUserControl()
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