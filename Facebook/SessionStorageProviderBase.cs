using System;
using System.Configuration.Provider;
using Facebook.Api;

namespace Facebook
{
    public abstract class SessionStorageProviderBase : ProviderBase
    {
        protected String KEY_FORMAT = "__FB_SESSION__{0}__{1}";

        public abstract SessionInfo LoadSession(String apiKey, Int64 uid);

        public abstract void SaveSession(String apiKey, SessionInfo sessionInfo);

        protected String GetKey(String apiKey, Int64 uid)
        {
            return String.Format(KEY_FORMAT, apiKey, uid);
        }
    }
}