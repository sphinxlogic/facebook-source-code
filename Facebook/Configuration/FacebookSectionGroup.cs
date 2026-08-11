using System;
using System.Configuration;
using Facebook.Desktop.Configuration;

namespace Facebook.Configuration
{
    /// <summary>Allows developers to programmatically access the various sections in the Facebook configuration.</summary>
    public class FacebookSectionGroup : ConfigurationSectionGroup
    {
        /// <summary>Contains configuration elements specific to the Facebook API.</summary>
        public static FacebookApiSection Api
        {
            get
            {
                var apiSection = ConfigurationManager.GetSection("facebook/api");
                if (apiSection != null) return (FacebookApiSection)apiSection;
                else return null;
            }
        }

        /// <summary>Gets a value representing whether a <see cref="FacebookApiSection" /> is configured for the current application.</summary>
        public static Boolean HasApiSection
        {
            get { return Api != null; }
        }

        /// <summary>Contains configuration elements specific to the desktop applications.</summary>
        public static FacebookDesktopSection Desktop
        {
            get
            {
                var apiSection = ConfigurationManager.GetSection("facebook/desktop");
                if (apiSection != null) return (FacebookDesktopSection)apiSection;
                else return null;
            }
        }

        /// <summary>Gets a value representing whether a <see cref="FacebookDesktopSection" /> is configured for the current application.</summary>
        public static Boolean HasDesktopSection
        {
            get { return Desktop != null; }
        }
    }
}