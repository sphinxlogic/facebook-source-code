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
        public ActionResult Links()
        {
            return this.View();
        }

        public ActionResult LinksPost(String comment, String link)
        {
            var result = this.FbContext.Links.Post(this.FbSession.Uid, link, comment);
            this.ViewData["ErrorMessage"] = "Sorry, there was an error posting your link";
            this.ViewData["SuccessMessage"] = "Your link has been posted!";
            this.ViewData["Title"] = "Post Link";
            this.AppendFbDomResourceId();            
            this.PostResult = this.PartialView("AsyncPostResult", result);
            return null;
        }
    }
}