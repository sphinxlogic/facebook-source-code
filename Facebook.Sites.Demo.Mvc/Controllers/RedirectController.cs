using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Facebook.Api.DemoSite.Models;
using System.Web.Configuration;

namespace Facebook.Api.DemoSite.Controllers
{
    public class RedirectController : Controller
    {
        public ActionResult Index(String id)
        {
            var redirect = WebConfigurationManager.AppSettings[String.Format("redirect.{0}", id)];
            if (String.IsNullOrEmpty(redirect)) return new HttpNotFoundResult(String.Format("Could not find a redirect for {0}", id));
            else return new FbRedirectResult(redirect);
        }
    }
}