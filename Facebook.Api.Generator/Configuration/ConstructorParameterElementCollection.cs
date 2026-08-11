using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    [ConfigurationCollection(typeof(ConstructorParameterElement))]
    public class ConstructorParameterElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConstructorParameterElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ConstructorParameterElement)element).Param;
        }
    }
}