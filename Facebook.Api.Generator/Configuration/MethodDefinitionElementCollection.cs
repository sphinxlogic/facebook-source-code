using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    [ConfigurationCollection(typeof(MethodDefinitionElement))]
    public class MethodDefinitionElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MethodDefinitionElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((MethodDefinitionElement)element).Method;
        }

        public MethodDefinitionElement GetMethod(string Name)
        {
            return (MethodDefinitionElement)BaseGet(Name);
        }

        public Boolean Exists(string Name)
        {
            return (BaseGet(Name) != null);
        }

    }
}