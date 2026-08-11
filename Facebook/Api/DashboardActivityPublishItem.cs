using System;

namespace Facebook.Api
{
    public class DashboardActivityPublishItem : FacebookArgs
    {
        public DashboardActivityPublishItem()
        {
            this.Activity = new DashboardItem();
        }

        protected DashboardItem Activity
        {
            get { return (DashboardItem)this["activity"]; }
            set { this["activity"] = value; }
        }

        public String Message
        {
            get { return this.Activity.Message; }
            set { this.Activity.Message = value; }
        }

        public DashboardActionLink ActionLink
        {
            get { return this.Activity.ActionLink; }
            set { this.Activity.ActionLink = value; }
        }
    }
}
