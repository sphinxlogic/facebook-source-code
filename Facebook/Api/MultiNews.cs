using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_multiGetNews_response_elt")]
    public class MultiNews : FacebookAttributeBasedObjectBase
    {
        public Int64 Uid
        {
            get { return this.GetValueTypeFromRoot<Int64>("key"); }
        }

        public FacebookAttributeBasedList<NewsItem> News
        {
            get { return this.GetFacebookObjectCollectionFromSelf<NewsItem>("Dashboard_multiGetNews_response_elt_elt"); }
        }
    }
}