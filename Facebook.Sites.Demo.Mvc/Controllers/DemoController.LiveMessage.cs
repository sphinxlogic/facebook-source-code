using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Facebook.Web;
using Facebook.Api.DemoSite.Models.Demo;
using Facebook.Api.DemoSite.Models;
using Facebook.Web.Mvc;

namespace Facebook.Api.DemoSite.Controllers
{
    public partial class DemoController : FacebookMvcController
    {
        public ActionResult LiveMessage()
        {
            this.ViewData["Uid"] = this.FbContext.Session.Uid;
            return this.View();
        }

        public ActionResult LiveMessageSend(String uid, String eventName, String message)
        {
            Int64 recipientUid = Int64.Parse(uid);
            var result = this.FbContext.LiveMessage.Send(recipientUid, eventName, message);
            this.ViewData["ErrorMessage"] = "Sorry, there was an error sending your message";
            this.ViewData["SuccessMessage"] = "Your message has been sent";
            this.ViewData["Title"] = "Send LiveMessage";
            this.AppendFbDomResourceId();            
            this.PostResult = this.PartialView("AsyncPostResult", result);
            return null;
        }
    }
}