using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    public class AttributeDefinitionElement : ConfigurationElement
    {
        [ConfigurationProperty("typeName", IsKey = true, IsRequired = true)]
        public String TypeName
        {
            get { return (String)this["typeName"]; }
            set { this["typeName"] = value; }
        }

        [ConfigurationProperty("params", IsRequired = false)]
        [ConfigurationCollection(typeof(ConstructorParameterElement))]
        public ConstructorParameterElementCollection Params
        {
            get { return (ConstructorParameterElementCollection)this["params"]; }
            set { this["params"] = value; }
        }
    }
}