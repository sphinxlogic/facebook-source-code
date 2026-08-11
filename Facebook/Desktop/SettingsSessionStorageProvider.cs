using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;
using Facebook.Api;

namespace Facebook.Desktop
{
    public class SettingsSessionStorageProvider : SessionStorageProviderBase
    {
        public override SessionInfo LoadSession(String apiKey, Int64 uid)
        {
            var config = (ClientSettingsSection)ConfigurationManager.GetSection("userSettings");
            if (config != null)
            {                
                var key = this.GetKey(apiKey, uid);
                return (SessionInfo)(Object)config.Settings.Get(key).Value.ValueXml;
            }
            else return null;
        }

        public override void SaveSession(String apiKey, SessionInfo sessionInfo)
        {
            throw new NotImplementedException();
        }
    }
}
