using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_getNews_response_elt")]
    public class NewsItem : FacebookAttributeBasedObjectBase
    {
        /// <summary>Intializes an instance of <see cref="NewsItem" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public NewsItem(XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="NewsItem" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public NewsItem(IDictionary<String, Object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="NewsItem" />.</summary>
        public NewsItem()
        {
        }

        public FacebookAttributeBasedList<DashboardItem> News
        {
            get { return this.GetFacebookObjectCollection<DashboardItem>("news", "Dashboard_getNews_response_elt_elt_elt"); }
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