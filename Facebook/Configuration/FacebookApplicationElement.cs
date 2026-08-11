using System;
using System.Configuration;

namespace Facebook.Configuration
{
    /// <summary>Configures Facebook applications that will be used by the client framework to make Facebook API requests.</summary>
    public class FacebookApplicationElement : ConfigurationElement
    {
        /// <summary>Gets or sets an optional environment value that specifies which development environment the configured application should be used for.</summary>
        /// <remarks>This property is only used for desktop applications, and is ignored for website applications.</remarks>
        [ConfigurationProperty("environment", IsKey = true, IsRequired = false)]
        public String Environment
        {
            get { return (String)this["environment"]; }
            set { this["environment"] = value; }
        }

        /// <summary>Gets or sets the API key for the application that is supplied by Facebook.</summary>
        [ConfigurationProperty("apiKey", IsKey = true, IsRequired = true)]
        public String ApiKey
        {
            get { return (String)this["apiKey"]; }
            set { this["apiKey"] = value; }
        }

        /// <summary>Gets or sets the application secret for the application that is supplied by Facebook.</summary>
        /// <remarks>This is required for web applications, but is not recommended to be populated for desktop applications. See the
        /// <a href="http://wiki.developers.facebook.com/index.php/Desktop_App_Auth_Process">Desktop App Auth Process</a> article for details.</remarks>
        [ConfigurationProperty("secret", IsRequired = false)]
        public String Secret
        {
            get { return (String)this["secret"]; }
            set { this["secret"] = value; }
        }

        /// <summary>Gets or sets the name of the Preload FQL configuration.</summary>
        [ConfigurationProperty("preloadFqlParam", IsRequired = false)]
        public String PreloadFqlParam
        {
            get { return (String)this["preloadFqlParam"]; }
            set { this["preloadFqlParam"] = value; }
        }
    }
}