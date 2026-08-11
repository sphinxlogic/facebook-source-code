using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    public class ExtraApiElement : ConfigurationElement
    {
        [ConfigurationProperty("doc", IsKey = true, IsRequired = true)]
        public String Doc
        {
            get { return (String)this["doc"]; }
            set { this["doc"] = value; }
        }
    }
}