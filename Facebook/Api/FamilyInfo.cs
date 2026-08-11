using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Facebook;

namespace Facebook.Api
{
    [FacebookObject("family_info")]
    public class FamilyInfo : FacebookObjectBase
    {
        public FamilyInfo(XElement content) :
            base(content)
        {
        }

        /// <summary>Intializes an instance of <see cref="FamilyInfo" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public FamilyInfo(IDictionary<string, object> dict) :
            base(dict)
        {
        }

        /// <summary>Intializes an instance of <see cref="FamilyInfo" />.</summary>
        public FamilyInfo()
        {
        }

        public String Relationship
        {
            get { return this.GetString("relationship"); }
            set { this.InnerDictionary["relationship"] = value; }
        }

        public Int64 Uid
        {
            get { return this.GetValueType<Int64>("uid"); }
            set { this.InnerDictionary["uid"] = value; }
        }

        public String Birthday
        {
            get { return this.GetString("birthday"); }
            set { this.InnerDictionary["birthday"] = value; }
        }

        public String Name
        {
            get { return this.GetString("name"); }
            set { this.InnerDictionary["name"] = value; }
        }

        public Int64 ProfileId
        {
            get { return this.GetValueType<Int64>("profile_id"); }
            set { this.InnerDictionary["profile_id"] = value; }
        }
    }
}