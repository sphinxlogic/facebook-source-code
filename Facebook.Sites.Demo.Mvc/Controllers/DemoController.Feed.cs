using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Facebook.Web.Mvc;

namespace Facebook.Api.DemoSite.Controllers
{
    public partial class DemoController : FacebookMvcController
    {
        public ActionResult Feed()
        {
            return View();
        }
    }
}