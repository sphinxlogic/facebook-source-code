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
        public ActionResult Notes()
        {
            return this.View();
        }

        [ValidateInput(false)]
        public ActionResult NotesCreate(String title, String content)
        {
            var result = this.FbContext.Notes.Create(this.FbContext.Session.Uid, title, content);
            this.ViewData["ErrorMessage"] = "Sorry, there was an error posting your note";
            this.ViewData["SuccessMessage"] = "Your note has been posted. Thanks!";
            this.ViewData["Title"] = "Create Note";
            this.AppendFbDomResourceId();            
            this.PostResult = this.PartialView("AsyncPostResult", result);
            return null;
        }
    }
}