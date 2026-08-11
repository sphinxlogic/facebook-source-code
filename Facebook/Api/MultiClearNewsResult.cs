using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_multiClearNews_response_elt")]
    public class MultiClearNewsResult : FacebookAttributeBasedObjectBase
    {
        /// <summary>Intializes an instance of <see cref="MultiClearNewsResult" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public MultiClearNewsResult(XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="MultiClearNewsResult" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public MultiClearNewsResult(IDictionary<String, Object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="MultiClearNewsResult" />.</summary>
        public MultiClearNewsResult()
        {
        }

        public Int64 Uid
        {
            get { return this.GetValueTypeFromRoot<Int64>("key"); }
        }

        public Boolean WasCleared
        {
            get { return this.GetValueTypeFromSelf<Boolean>(); }
        }
    }
}