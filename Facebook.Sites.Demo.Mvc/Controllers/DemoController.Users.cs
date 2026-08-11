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
        public ActionResult Users()
        {
            using (var batch = Batch.Start(this.FbContext))
            {
                var uids = new String[] { this.FbContext.Session.Uid.ToString() };
                var fields = UserProperty.FirstName | UserProperty.LastName;
                var standardFields = UserProperty.FirstName | UserProperty.LastName | UserProperty.Name | UserProperty.TimeZone | UserProperty.Birthday | UserProperty.Sex | UserProperty.Affiliations | UserProperty.Locale | UserProperty.ProfileUrl | UserProperty.ProxiedEmail;

                var getInfoResponse = this.FbContext.Users.GetInfo(fields, uids);
                var getLoggedInUserResponse = this.FbContext.Users.GetLoggedInUser();
                var getStandardInfoResponse = this.FbContext.Users.GetStandardInfo(standardFields, uids);
                var hasEmailPermissionResponse = this.FbContext.Users.HasAppPermission("email");
                var hasOfflineAccessPermissionResponse = this.FbContext.Users.HasAppPermission("offline_access");
                var hasStatusUpdatePermissionResponse = this.FbContext.Users.HasAppPermission("status_update");
                var hasPhotoUploadPermissionResponse = this.FbContext.Users.HasAppPermission("photo_upload");
                var hasCreateListingPermissionResponse = this.FbContext.Users.HasAppPermission("create_listing");
                var hasCreateEventPermissionResponse = this.FbContext.Users.HasAppPermission("create_event");
                var hasRsvpEventPermissionResponse = this.FbContext.Users.HasAppPermission("rsvp_event");
                var hasSmsPermissionResponse = this.FbContext.Users.HasAppPermission("sms");
                var hasShareItemPermissionResponse = this.FbContext.Users.HasAppPermission("share_item");
                var hasReadStreamPermissionResponse = this.FbContext.Users.HasAppPermission("read_stream");

                var response = batch.CompleteDebug();

                return View(new UsersIndexData
                {
                    GetInfo = getInfoResponse.Value,
                    GetLoggedInUser = getLoggedInUserResponse.Value,
                    GetStandardInfo = getStandardInfoResponse.Value,
                    HasEmailPermission = hasEmailPermissionResponse.Value,
                    HasOfflineAccessPermission = hasOfflineAccessPermissionResponse.Value,
                    HasStatusUpdatePermission = hasStatusUpdatePermissionResponse.Value,
                    HasPhotoUploadPermission = hasPhotoUploadPermissionResponse.Value,
                    HasCreateListingPermission = hasCreateListingPermissionResponse.Value,
                    HasCreateEventPermission = hasCreateEventPermissionResponse.Value,
                    HasRsvpEventPermission = hasRsvpEventPermissionResponse.Value,
                    HasSmsPermission = hasSmsPermissionResponse.Value,
                    HasShareItemPermission = hasShareItemPermissionResponse.Value,
                    HasReadStreamPermission = hasReadStreamPermissionResponse.Value,
                    Response = response
                });
            }
        }

        [Authorize]
        public ActionResult RevokeExtendedPermission()
        {
            if (!this.FbContext.HasSession)
            {
                return new FbRedirectResult(this.FbContext.GetAuthorizationUrl("/demo/users"));
            }
            this.FbContext.Auth.RevokeExtendedPermission(this.FbContext.Session.Uid, this.Request.Params["perm_name"]);
            return this.RedirectToAction("Users");
        }
    }
}