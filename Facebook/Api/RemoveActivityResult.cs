using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_removeActivity_response_elt")]
    public class RemoveActivityResult : FacebookAttributeBasedObjectBase
    {
        /// <summary>Intializes an instance of <see cref="RemoveActivityResult" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public RemoveActivityResult(XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="RemoveActivityResult" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public RemoveActivityResult(IDictionary<String, Object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="RemoveActivityResult" />.</summary>
        public RemoveActivityResult()
        {
        }

        public Int64 Uid
        {
            get { return this.GetValueTypeFromRoot<Int64>("key"); }
        }

        public Boolean WasRemoved
        {
            get { return this.GetValueTypeFromSelf<Boolean>(); }
        }
    }
}