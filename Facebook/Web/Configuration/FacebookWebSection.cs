using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace Facebook.Web.Configuration
{
    public class FacebookWebSection : ConfigurationSection
    {
        /// <summary>Gets or sets the mode that controls if and when Application.GetPublicInfo is invoked.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <listheader><see cref="GetPublicInfoMode" /> values:</listheader>
        /// <item><term>ApplicationInitialization</term><description>A request to <c>Application.GetPublicInfo</c> is made when the ASP.NET application is first initialized.</description></item>
        /// <item><term>FirstUse</term><description>A the request is made the first time a value is needed.</description></item>
        /// <item><term>Never</term><description>The request will never be made automatically, and must be manually invoked by the client application if information is needed.</description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="GetPublicInfoMode" />
        [ConfigurationProperty("getPublicInfoMode")]
        public GetPublicInfoMode GetPublicInfoMode
        {
            get { return (GetPublicInfoMode)this["getPublicInfoMode"]; }
            set { this["getPublicInfoMode"] = value; }
        }

        /// <summary>Gets or sets a value representing whether the Facebook API Client will set the <see cref="FormsAuthentication" /> cookie if it detects
        /// a valid Facebook session.</summary>
        [ConfigurationProperty("useFormsAuthentication", IsRequired = false, DefaultValue = true)]
        public Boolean UseFormsAuthentication
        {
            get { return (Boolean)this["useFormsAuthentication"]; }
            set { this["useFormsAuthentication"] = value; }
        }

        public static Boolean IsConfigured
        {
            get { return FacebookWebSection.Current != null; }
        }

        /// <summary>Gets a reference to the <see cref="FacebookWebSection" /> configured for the current application.</summary>
        public static FacebookWebSection Current
        {
            get
            {
                var apiSection = ConfigurationManager.GetSection("facebook/web");
                if (apiSection != null) return (FacebookWebSection)apiSection;
                else return null;
            }
        }

    }
}
