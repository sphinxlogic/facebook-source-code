using System;
using System.Configuration;

namespace Facebook.Desktop.Configuration
{
    /// <summary>Contains configuration elements specific to desktop applications.</summary>
    public class FacebookDesktopSection : ConfigurationSection
    {
        [ConfigurationProperty("authentication")]
        [ConfigurationCollection(typeof(DesktopAuthenticationElement))]
        public DesktopAuthenticationElementCollection Authentication
        {
            get { return (DesktopAuthenticationElementCollection)this["authentication"]; }
            set { this["authentication"] = value; }
        }
    }
}