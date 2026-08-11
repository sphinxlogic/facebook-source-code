using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Facebook;

namespace Facebook.Api
{
    [FacebookObjectAttribute("action_link")]
    public class DashboardActionLink : FacebookAttributeBasedObjectBase
    {
        /// <summary>Intializes an instance of <see cref="ActionLink" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public DashboardActionLink(System.Xml.Linq.XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="ActionLink" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public DashboardActionLink(System.Collections.Generic.IDictionary<string, object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="ActionLink" />.</summary>
        public DashboardActionLink()
        {
        }

        public String Text
        {
            get
            {
                return this.GetString("text");
            }
            set
            {
                this.InnerDictionary["text"] = value;
            }
        }

        public String Href
        {
            get
            {
                return this.GetString("href");
            }
            set
            {
                this.InnerDictionary["href"] = value;
            }
        }
    }
}
