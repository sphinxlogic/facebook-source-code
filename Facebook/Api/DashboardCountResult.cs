using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facebook.Api
{
    [FacebookObject("Dashboard_multiGetCount_response_elt")]
    public class DashboardMultiGetCountResult : FacebookAttributeBasedObjectBase
    {
        public DashboardMultiGetCountResult() { }

        public Int64 Uid { get { return this.GetValueTypeFromRoot<Int64>("key"); } }
        public Int64 Count { get { return this.GetValueTypeFromSelf<Int64>(); } }
    }

    [FacebookObject("Dashboard_multiIncrementCount_response_elt")]
    public class DashboardMultiIncrementCountResult : DashboardCountResult
    {
        public DashboardMultiIncrementCountResult() { }
    }

    [FacebookObject("Dashboard_multiDecrementCount_response_elt")]
    public class DashboardMultiDecrementCountResult : DashboardCountResult
    {
        public DashboardMultiDecrementCountResult() { }
    }

    [FacebookObject("Dashboard_multiSetCount_response_elt")]
    public class DashboardMultiSetCountResult : DashboardCountResult
    {
        public DashboardMultiSetCountResult() { }
    }

    public class DashboardCountResult : FacebookAttributeBasedObjectBase
    {
        /// <summary>Intializes an instance of <see cref="DashboardCountResult" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public DashboardCountResult(XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="DashboardCountResult" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public DashboardCountResult(IDictionary<String, Object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="DashboardCountResult" />.</summary>
        public DashboardCountResult()
        {
        }

        public Int64 Uid
        {
            get { return this.GetValueTypeFromRoot<Int64>("key"); }
        }

        public Boolean Success
        {
            get { return this.GetValueTypeFromSelf<Boolean>(); }
        }

    }
}
