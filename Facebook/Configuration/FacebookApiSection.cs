using System;
using System.Configuration;

namespace Facebook.Configuration
{
    /// <summary>Contains configuration elements specific to the Facebook API.</summary>
    public class FacebookApiSection : ConfigurationSection
    {
        /// <summary>Gets or sets a collection of <see cref="FacebookApplicationElement" /> elements that define the Facebook application information to be used by the application.</summary>
        [ConfigurationProperty("applications", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(FacebookApplicationElement))]
        public FacebookApplicationElementCollection Applications
        {
            get { return (FacebookApplicationElementCollection)this["applications"]; }
            set { this["applications"] = value; }
        }

        [ConfigurationProperty("authUrl", IsRequired = false)]
        public String AuthUrl
        {
            get { return (String)this["authUrl"]; }
            set { this["authUrl"] = value; }
        }
    }
}