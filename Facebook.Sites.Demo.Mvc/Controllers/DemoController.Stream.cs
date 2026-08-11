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
        [Authorize]
        public ActionResult Stream()
        {
            using (var batch = Batch.Start(this.FbContext))
            {
                var stream = this.FbContext.Stream.Get(3);
                var filters = this.FbContext.Stream.GetFilters();

                var response = batch.CompleteDebug();

                return this.View(new StreamIndexData { Filters = filters.Value, Stream = stream.Value });
            }
        }

        public ActionResult StreamPublish(String message)
        {
            var result = this.FbContext.Stream.Publish(message);
            this.ViewData["ErrorMessage"] = "Sorry, there was a problem publishing to your feed";
            this.ViewData["SuccessMessage"] = "Message successfully published to your feed!";
            this.ViewData["Title"] = "Publish Stream";
            this.AppendFbDomResourceId();
            this.PostResult = this.PartialView("AsyncPostResult", result);
            return null;
        }

        public ActionResult StreamRemove(String postId)
        {
            var result = this.FbContext.Stream.Remove(postId);
            return this.RedirectToAction("Stream");
        }

        public ActionResult StreamAddComment(String postId, String comment)
        {
            var result = this.FbContext.Stream.AddComment(comment, postId);
            this.ViewData["ErrorMessage"] = "Sorry, there was a problem adding your comment to the post";
            this.ViewData["SuccessMessage"] = "Comment successfully added";
            this.ViewData["Title"] = "Add Comment";
            this.AppendFbDomResourceId();
            this.PostResult = this.PartialView("AsyncPostResult", result);
            return null;
        }

        public ActionResult StreamRemoveComment(String commentId)
        {
            var result = this.FbContext.Stream.RemoveComment(commentId);
            return this.RedirectToAction("Stream");
        }

        public ActionResult StreamAddLike(String postId)
        {
            var result = this.FbContext.Stream.AddLike(postId);
            return this.RedirectToAction("Stream");
        }

        public ActionResult StreamRemoveLike(String postId)
        {
            var result = this.FbContext.Stream.RemoveLike(postId);
            return this.RedirectToAction("Stream");
        }
      
    }
}