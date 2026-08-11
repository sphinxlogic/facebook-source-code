using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Api.DemoSite.Models.Demo
{
    public class UsersIndexData
    {
        public FacebookList<User> GetInfo { get; set; }
        public Int64 GetLoggedInUser { get; set; }
        public FacebookList<User> GetStandardInfo { get; set; }
        public Boolean HasEmailPermission { get; set; }
        public Boolean HasOfflineAccessPermission { get; set; }
        public Boolean HasStatusUpdatePermission { get; set; }
        public Boolean HasPhotoUploadPermission { get; set; }
        public Boolean HasCreateListingPermission { get; set; }
        public Boolean HasCreateEventPermission { get; set; }
        public Boolean HasRsvpEventPermission { get; set; }
        public Boolean HasSmsPermission { get; set; }
        public Boolean HasShareItemPermission { get; set; }
        public Boolean HasReadStreamPermission { get; set; }
        public String Response { get; set; }
    }
}
