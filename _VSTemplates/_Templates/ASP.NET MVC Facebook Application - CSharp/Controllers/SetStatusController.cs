using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Facebook.Web.Mvc;

namespace $rootnamespace$.Controllers
{
    [Authorize]
    public class SetStatusController : FacebookMvcController
    {
        //
        // GET: /SetStatus/

        public ActionResult Index(String previousResult)
        {
            this.ViewData["PreviousResult"] = previousResult;
            return View();
        }

        public ActionResult Set(String status)
        {
            var result = this.FbContext.Status.Set(status);

            String resultMessage = null;
            if (result.IsError) resultMessage = String.Format("There was an error setting your status: {0}", result.ResponseException.Message);
            else resultMessage = "Your status was set successfully.";

            return this.RedirectToAction("Index", new { previousResult = resultMessage });
        }
    }
}
