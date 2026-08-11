using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    [ConfigurationCollection(typeof(AttributeDefinitionElement))]
    public class AttributeDefinitionElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AttributeDefinitionElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((AttributeDefinitionElement)element).TypeName;
        }

        public AttributeDefinitionElement GetElement(int id)
        {
            return ((AttributeDefinitionElement)BaseGet(id));
        }
    }
}