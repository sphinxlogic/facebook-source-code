using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_getGlobalNews_response_elt")]
    public class GlobalNewsItem : FacebookAttributeBasedObjectBase
    {
        /// <summary>Intializes an instance of <see cref="GlobalNewsItem" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public GlobalNewsItem(XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="GlobalNewsItem" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public GlobalNewsItem(IDictionary<String, Object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="GlobalNewsItem" />.</summary>
        public GlobalNewsItem()
        {
        }

        public FacebookAttributeBasedList<DashboardItem> News
        {
            get { return this.GetFacebookObjectCollection<DashboardItem>("news", "Dashboard_getGlobalNews_response_elt_elt_elt"); }
            set { this.InnerDictionary["news"] = value; }
        }

        public String Image
        {
            get { return this.GetString("image"); }
            set { this.InnerDictionary["image"] = value; }
        }

        public Int64 Id
        {
            get { return this.GetValueTypeFromRoot<Int64>("key"); }            
        }
    }
}