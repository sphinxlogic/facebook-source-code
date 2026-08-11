using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Api.DemoSite.Models.Demo
{
    public class StreamIndexData
    {
        public FacebookList<StreamFilter> Filters { get; set; }
        public StreamData Stream { get; set; }
    }
}
