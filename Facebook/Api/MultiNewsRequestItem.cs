using System;
using System.Collections.Generic;

namespace Facebook.Api
{
    [Serializable]
    public class MultiNewsRequestItem
    {
        public Int64 Uid { get; set; }

        public Int64[] NewsIds { get; set; }
    }
}