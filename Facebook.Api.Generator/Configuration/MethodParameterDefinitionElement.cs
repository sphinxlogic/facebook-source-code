using System;
using System.Collections.Generic;
using System.Configuration;

namespace Facebook.Api.Generator.Configuration
{
    public class MethodParameterDefinitionElement : ConfigurationElement
    {
        [ConfigurationProperty("nameSuffix", IsKey = true, IsRequired = false)]
        public String NameSuffix
        {
            get { return (String)this["nameSuffix"]; }
            set { this["nameSuffix"] = value; }
        }

        [ConfigurationProperty("params", IsKey = true, IsRequired = false)]
        public String Params
        {
            get { return (String)this["params"]; }
            set { this["params"] = value; }
        }

        [ConfigurationProperty("applicationType", IsRequired = false, DefaultValue = ApplicationType.Desktop | ApplicationType.Website)]
        public ApplicationType ApplicationType
        {
            get { return (ApplicationType)this["applicationType"]; }
            set { this["applicationType"] = value; }
        }

        public Boolean ContainsParameters(List<String> paramList)
        {
            String[] excludedParams = this.Params.Split(',');
            if (excludedParams.Length != paramList.Count)
                return false;
            foreach (String param in excludedParams)
            {
                if (!paramList.Contains(param))
                    return false;
            }
            return true;
        }
    }
}