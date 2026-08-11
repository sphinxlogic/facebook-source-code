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
        public ActionResult Fql()
        {
            String fqlQuery = String.Format("select uid, first_name, last_name, name, pic_small, pic_big, pic_square, pic, affiliations, profile_update_time, timezone, religion, birthday, sex, hometown_location, meeting_sex, meeting_for, relationship_status, significant_other_id, political, current_location, activities, interests, is_app_user, music, tv, movies, books, quotes, about_me, hs_info, education_history, status, has_added_app, online_presence, locale, proxied_email, profile_url, pic_small_with_logo, pic_big_with_logo, pic_square_with_logo, pic_with_logo, allowed_restrictions, verified FROM user WHERE {0} in (uid);", this.FbContext.Session.Uid);
            var result = this.FbContext.Fql.Query<User>(fqlQuery);
            return View(result.Value.FirstOrDefault());
        }

        public ActionResult FqlQueryXml(String fqlQuery)
        {
            String queryResult = String.Empty;
            FacebookResponse<System.Xml.Linq.XElement> result = this.FbContext.Fql.QueryXml(fqlQuery);
            if (!result.IsError) 
            {
                using (var reader = result.Value.CreateReader())
                {
                    if (reader.Read()) { queryResult = reader.ReadOuterXml(); } 
                }
            }

            this.ViewData["ErrorMessage"] = "Error with FQL query";
            this.ViewData["SuccessMessage"] = String.Format("FQL Query successful, {0}", HttpUtility.HtmlEncode(queryResult));
            this.ViewData["Title"] = "FQL Query XML Result";
            this.AppendFbDomResourceId();            
            this.PostResult = this.PartialView("AsyncPostResult", result);
            return null;
        }

        public ActionResult FqlQueryJson(String fqlQuery)
        {
            String queryResult = String.Empty;
            FacebookJsonResponse<System.Xml.Linq.XElement> result = this.FbContext.Fql.QueryJson(fqlQuery);
            if (!result.IsError) { queryResult = result.Value; }
 
            this.ViewData["ErrorMessage"] = "Error with FQL query";
            this.ViewData["SuccessMessage"] = String.Format("FQL Query successful, {0}", HttpUtility.HtmlEncode(queryResult));
            this.ViewData["Title"] = "FQL Query JSON Result";
            this.AppendFbDomResourceId();            
            this.PostResult = this.PartialView("AsyncPostResult", result);
            return null;
        }

        public ActionResult FqlQueryUser(String uid)
        {
            return this.View();
        }
    }
}