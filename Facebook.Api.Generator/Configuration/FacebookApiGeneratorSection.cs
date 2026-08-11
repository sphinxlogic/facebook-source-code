using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Facebook.Api.Generator.Configuration
{
    public class FacebookApiGeneratorSection : ConfigurationSection
    {
        /// <summary>Gets or sets a list of methods that should be excluded from being generated.</summary>
        [ConfigurationProperty("extraApis")]
        [ConfigurationCollection(typeof(ExtraApiElement))]
        public ExtraApiElementCollection ExtraApis
        {
            get { return (ExtraApiElementCollection)this["extraApis"]; }
            set { this["extraApis"] = value; }
        }

        /// <summary>Gets or sets a list of methods that should be excluded from being generated.</summary>
        [ConfigurationProperty("excludedMethods")]
        [ConfigurationCollection(typeof(MethodDefinitionElement))]
        public MethodDefinitionElementCollection ExcludedMethods
        {
            get { return (MethodDefinitionElementCollection)this["excludedMethods"]; }
            set { this["excludedMethods"] = value; }
        }

        [ConfigurationProperty("explicitMethodDefinitions")]
        [ConfigurationCollection(typeof(MethodDefinitionElement))]
        public MethodDefinitionElementCollection ExplicitMethodDefinitions
        {
            get { return (MethodDefinitionElementCollection)this["explicitMethodDefinitions"]; }
            set { this["explicitMethodDefinitions"] = value; }
        }

        /// <summary>
        /// Gets or sets a list of methods to grab from the API wiki page
        /// </summary>
        [ConfigurationProperty("methodPages")]
        [ConfigurationCollection(typeof(MethodDefinitionElement))]
        public MethodDefinitionElementCollection MethodPages
        {
            get { return (MethodDefinitionElementCollection)this["methodPages"]; }
            set { this["methodPages"] = value; }
        }

        private static FacebookApiGeneratorSection _current;
        /// <summary>Gets a reference to the <see cref="FacebookApiGeneratorSection" /> configured for the current application.</summary>
        public static FacebookApiGeneratorSection Current
        {
            get
            {
                if (FacebookApiGeneratorSection._current == null)
                {
                    var config = ConfigurationManager.OpenExeConfiguration("Facebook.Api.Generator.exe");
                    var sections = config.Sections.OfType<ConfigurationSection>().Where(section => section.GetType() == typeof(FacebookApiGeneratorSection)).ToList();
                    if (sections.Count > 1)
                    {
                        throw new ApplicationException(String.Format("Too many instances of FacebookApiGeneratorSection were found. Expected 1, found {0}.", sections.Count));
                    }
                    else if (sections.Count == 0)
                    {
                        throw new ApplicationException("A configuration section of the type FacebookApiGeneratorSection was not found in the current configuration.");
                    }
                    else
                    {
                        FacebookApiGeneratorSection._current = (FacebookApiGeneratorSection)sections.Single();
                    }
                }
                return FacebookApiGeneratorSection._current;
            }
        }

    }
}