using System;
using System.Configuration;
using System.Linq;

namespace Facebook.Desktop.Configuration
{
    [ConfigurationCollection(typeof(DesktopAuthenticationElement))]
    public class DesktopAuthenticationElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DesktopAuthenticationElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((DesktopAuthenticationElement)element).ApiKey;
        }

        /// <summary>Gets the <see cref="DesktopAuthenticationElement" /> configured for the specified <paramref name="apiKey"/>.</summary>        
        /// <param name="apiKey">The API key for the current HTTP request.</param>
        /// <returns>The <see cref="DesktopAuthenticationElement" /> configured for the specified <paramref name="apiKey"/>.</returns>
        public DesktopAuthenticationElement GetByApiKey(String apiKey)
        {
            return this.OfType<DesktopAuthenticationElement>().SingleOrDefault(app => app.ApiKey == apiKey);
        }
    }
}