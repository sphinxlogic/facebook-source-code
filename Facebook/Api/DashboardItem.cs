using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    public class DashboardItem : FacebookArgsObjectBase
    {        
        /// <summary>Intializes an instance of <see cref="News" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public DashboardItem(XElement content) : 
                base(content) {
        }

        /// <summary>Intializes an instance of <see cref="News" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public DashboardItem(IDictionary<String, Object> dict) : 
                base(dict) {
        }

        /// <summary>Intializes an instance of <see cref="News" />.</summary>
        public DashboardItem()
        {
        }

        public String Message
        {
            get { return this.GetString("message"); }
            set { this["message"] = value; }
        }

        public DashboardActionLink ActionLink
        {
            get { return this.GetFacebookObject<DashboardActionLink>("action_link"); }
            set { this["action_link"] = value; }
        }
    }
}