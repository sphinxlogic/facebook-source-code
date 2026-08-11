using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_getActivity_response_elt")]
    public class DashboardActivity : FacebookAttributeBasedObjectBase
    {
        /// <summary>Intializes an instance of <see cref="News" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public DashboardActivity(XElement content) : 
                base(content) {
        }

        /// <summary>Intializes an instance of <see cref="News" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public DashboardActivity(IDictionary<String, Object> dict) : 
                base(dict) {
        }

        /// <summary>Intializes an instance of <see cref="News" />.</summary>
        public DashboardActivity()
        {
        }

        public Int64 Id
        {
            get { return this.GetValueTypeFromRoot<Int64>("key"); }
        }

        public String Message
        {
            get { return this.GetString("message"); }
            set { this.InnerDictionary["message"] = value; }
        }

        public DashboardActionLink ActionLink
        {
            get { return this.GetFacebookObject<DashboardActionLink>("action_link"); }
            set { this.InnerDictionary["action_link"] = value; }
        }
    }
}
