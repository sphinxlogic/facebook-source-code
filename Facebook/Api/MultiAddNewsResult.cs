using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_multiAddNews_response_elt")]
    public class MultiAddNewsResult : FacebookAttributeBasedObjectBase
    {        
        /// <summary>Intializes an instance of <see cref="NewsItem" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public MultiAddNewsResult(XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="NewsItem" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public MultiAddNewsResult(IDictionary<String, Object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="NewsItem" />.</summary>
        public MultiAddNewsResult()
        {
        }

        public Int64 Uid
        {
            get { return this.GetValueTypeFromRoot<Int64>("key"); }
        }

        public Int64 NewsId
        {
            get { return this.GetValueTypeFromSelf<Int64>(); }
        }

        public Boolean WasAdded
        {
            get { return this.NewsId > 0; }
        }
    }
}