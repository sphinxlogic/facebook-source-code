using System;
using System.Configuration;
using Facebook.Desktop;
using System.Diagnostics;
using System.Threading;

namespace Facebook.Api.Generator.Tests
{
    public static class Helpers
    {
        public static FacebookDesktopContext GetDesktopContext()
        {
            return FacebookDesktopContext.Current;
        }

        public static Boolean EstablishSession(ref FacebookDesktopContext context)
        {
            if (String.IsNullOrEmpty(Sessions.Default.AuthToken) || Sessions.Default.Session == null || Sessions.Default.Session.Expires < DateTime.Now)
            {
                Sessions.Default.AuthToken = context.Auth.CreateToken();

                var loginUrl = String.Format("http://www.facebook.com/login.php?api_key={0}&v=1.0&auth_token={1}", context.ApiKey, Sessions.Default.AuthToken);
                var process = Process.Start(loginUrl);
                var startChecking = DateTime.Now.AddSeconds(2);

                while (DateTime.Now < startChecking) System.Threading.Thread.Sleep(0);

                var tries = 0;
                while (tries < 10)
                {
                    var sessionInfo = context.Auth.GetSession(Sessions.Default.AuthToken);
                    if (!sessionInfo.IsError && !String.IsNullOrEmpty(sessionInfo.Value.SessionKey))
                    {
                        Sessions.Default.Session = sessionInfo.Value;
                        return true;
                    }
                    else
                    {
                        tries++;
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                return false;
            }
            else
            {
                context.InitSession(Sessions.Default.Session);
                return true;
            }
        }
    }
}