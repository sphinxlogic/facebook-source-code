using System;
using System.Configuration;

namespace Facebook.Desktop.Configuration
{
    public class DesktopAuthenticationElement : ConfigurationElement
    {
        /// <summary>Gets or sets which API key the element refers to.</summary>
        [ConfigurationProperty("apiKey", IsKey = true, IsRequired = true)]
        public String ApiKey
        {
            get { return (String)this["apiKey"]; }
            set { this["apiKey"] = value; }
        }

        /// <summary>Gets or sets the base url to use to handle authentication calls for the application.</summary>
        [ConfigurationProperty("authUrl")]
        public Uri AuthUrl
        {
            get { return (Uri)this["authUrl"]; }
            set { this["authUrl"] = value; }
        }

        /// <summary>Gets or sets the relative url or complete URI used to proxy calls to Auth.createToken.</summary>
        /// <remarks>The default value is <c>Auth/CreateToken</c>.</remarks>
        [ConfigurationProperty("createTokenUrl", IsRequired = false, DefaultValue = "Auth/CreateToken")]
        public String CreateTokenUrl
        {
            get { return (String)this["createTokenUrl"]; }
            set { this["createTokenUrl"] = value; }
        }

        /// <summary>Gets or sets the relative url or complete URI used to proxy calls to Auth.getSession.</summary>
        /// <remarks>The default value is <c>Auth/GetSession</c>.</remarks>
        [ConfigurationProperty("getSessionUrl", IsRequired = false, DefaultValue = "Auth/GetSession")]
        public String GetSessionUrl
        {
            get { return (String)this["getSessionUrl"]; }
            set { this["getSessionUrl"] = value; }
        }
    }
}