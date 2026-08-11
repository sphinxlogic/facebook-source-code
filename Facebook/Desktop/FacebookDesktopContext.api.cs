using System;
using Facebook.Api;

namespace Facebook.Desktop
{
    public partial class FacebookDesktopContext : FacebookContextBase<StaticDictionaryCache, SessionInfo>
    {
        private static FacebookDesktopContext _current;
        public static FacebookDesktopContext Current
        {
            get
            {
                if (FacebookDesktopContext._current == null)
                {
                    var app = Facebook.Configuration.FacebookSectionGroup.Api.Applications.GetCurrent();
                    FacebookDesktopContext._current = new FacebookDesktopContext(app.ApiKey, app.Secret);
                }
                return FacebookDesktopContext._current;
            }
            set { FacebookDesktopContext._current = value; }
        }
    }
}