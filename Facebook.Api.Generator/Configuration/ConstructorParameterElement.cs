using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    public class ConstructorParameterElement : ConfigurationElement
    {
        [ConfigurationProperty("param", IsKey = true, IsRequired = false)]
        public String Param
        {
            get { return (String)this["param"]; }
            set { this["param"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = false)]
        public String Value
        {
            get { return (String)this["value"]; }
            set { this["value"] = value; }
        }
    }
}