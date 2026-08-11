using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    [ConfigurationCollection(typeof(ExtraApiElement))]
    public class ExtraApiElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExtraApiElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ExtraApiElement)element).Doc;
        }
    }
}