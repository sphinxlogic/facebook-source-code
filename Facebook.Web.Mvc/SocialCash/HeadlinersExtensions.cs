using System;
using System.Linq;
using System.Text;

namespace Facebook.Web.Mvc.SocialCash
{
    public static class HeadlinersExtensions
    {
        public static String RenderHeadlinersAd(this FbIFrameHelper iframe, String appCode, String adTag)
        {
            return String.Format(Content.IntegrationIFrame, appCode, adTag, HeadlinersExtensions.RequiredParams);
        }

        public static String RenderHeadlinersAd(this FbmlHelper fbml, String appCode, String adTag)
        {
            return String.Format(Content.IntegrationFbml, appCode, adTag, HeadlinersExtensions.RequiredParams);
        }

        private static String RequiredParams
        {
            get
            {
                var session = FacebookHttpSession.Current;
                var paramString = new StringBuilder();
                foreach (var key in session.FbSig.Keys.OfType<String>())
                {
                    paramString.AppendFormat("&{0}={1}", key, session.FbSig[key]);
                }
                return paramString.ToString();
            }
        }
    }
}