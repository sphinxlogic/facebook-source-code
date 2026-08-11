using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Offline
{
    public partial class FacebookOfflineAccessContext : FacebookContextBase<StaticDictionaryCache, Api.SessionInfo>
    {
        public FacebookOfflineAccessContext(ApplicationType applicationType, String apiKey, String secret) :
            base(applicationType, apiKey, secret)
        {
        }

        public FacebookOfflineAccessContext(ApplicationType applicationType, String apiKey, String secret, String version) :
            base(applicationType, apiKey, secret, version)
        {
        }
    }
}
