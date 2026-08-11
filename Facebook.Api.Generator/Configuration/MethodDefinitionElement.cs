using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    public class MethodDefinitionElement : ConfigurationElement
    {
        [ConfigurationProperty("method", IsKey = true, IsRequired = true)]
        public String Method
        {
            get { return (String)this["method"]; }
            set { this["method"] = value; }
        }

        [ConfigurationProperty("applicationType", IsRequired = false, DefaultValue = ApplicationType.Desktop | ApplicationType.Website)]
        public ApplicationType ApplicationType
        {
            get { return (ApplicationType)this["applicationType"]; }
            set { this["applicationType"] = value; }
        }

        [ConfigurationProperty("overloads", IsRequired = false)]
        [ConfigurationCollection(typeof(MethodParameterDefinitionElement))]
        public MethodParameterDefinitionElementCollection Overloads
        {
            get { return (MethodParameterDefinitionElementCollection)this["overloads"]; }
            set { this["overloads"] = value; }
        }

        [ConfigurationProperty("attributes", IsRequired = false)]
        [ConfigurationCollection(typeof(AttributeDefinitionElement))]
        public AttributeDefinitionElementCollection Attributes
        {
            get { return (AttributeDefinitionElementCollection)this["attributes"]; }
            set { this["attributes"] = value; }
        }
        
        [ConfigurationProperty("description", IsRequired = false, DefaultValue = "")]
        public String Description
        {
            get { return (String)this["description"]; }
            set { this["description"] = value; }
        }

        [ConfigurationProperty("parameters", IsRequired = false, DefaultValue = "")]
        public String Parameters
        {
            get { return (String)this["parameters"]; }
            set { this["parameters"] = value; }
        }
        
    }
}