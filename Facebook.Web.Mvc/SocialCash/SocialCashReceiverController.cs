using System;
using System.Web.Mvc;
using System.Web.UI;

namespace Facebook.Web.Mvc.SocialCash
{
    public class SocialCashReceiverController : Controller
    {
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Any, VaryByParam = "")]
        public ActionResult Index()
        {
            return this.Content(Facebook.Web.Mvc.SocialCash.Content.Receiver, "text/html");
        }
    }
}
