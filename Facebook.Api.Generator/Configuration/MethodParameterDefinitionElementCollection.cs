using System;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    [ConfigurationCollection(typeof(MethodParameterDefinitionElement))]
    public class MethodParameterDefinitionElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MethodParameterDefinitionElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((MethodParameterDefinitionElement)element).Params;
        }

        public MethodParameterDefinitionElement GetParameter(int i)
        {
            return (MethodParameterDefinitionElement)BaseGet(i);
        }

        public MethodParameterDefinitionElement GetParameter(string Name)
        {
            return (MethodParameterDefinitionElement)BaseGet(Name);
        }

        public Boolean Exists(string Name)
        {
            return (BaseGet(Name) != null);
        }
    }
}