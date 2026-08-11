using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Facebook.Api.DemoSite.Models.Demo
{
    public class AdminIndexData
    {
        public Int64 NotificationsPerDay { get; set; }
        public Int64 AnnoucementNotificationsPerWeek { get; set; }
        public Int64 RequestsPerDay { get; set; }
        public Int64 EmailsPerDay { get; set; }
        public Int64 EmailDisableMessageLocation { get; set; }

        public Dictionary<String, String> AppProperties { get; set; }

        public FacebookList<Metrics> Metrics { get; set; }

        public String RestrictionInfo { get; set; }
    }
}
