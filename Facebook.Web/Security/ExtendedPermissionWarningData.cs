using System;

namespace Facebook.Web.Security
{
    public class ExtendedPermissionWarningData
    {
        public ExtendedPermission Required { get; set; }
        public ExtendedPermission Optional { get; set; }
        public Boolean RequiresAuthorization { get; set; }
    }
}