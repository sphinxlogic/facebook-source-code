using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Api.Controllers
{
    public partial class UsersController : FacebookApiController
    {
        /// <summary>Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.</summary>
        /// <param name="uids">List of <a href="/index.php/User_ID" title="User ID">user IDs</a>. This is a comma-separated list of user IDs.</param>
        /// <param name="fields">List of desired fields in return. This is a comma-separated list of field strings.</param>
        public FacebookResponse<FacebookList<User>> GetInfo(UserProperty propertySet, params Object[] uids)
        {
            return this.GetInfo(propertySet, uids.ToList().ConvertAll(uid => Convert.ToInt64(uid)).ToArray());
        }

        public FacebookResponse<FacebookList<User>> GetInfo(UserProperty propertySet, IEnumerable<Int64> uids)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            var fields = propertySet.ToFieldNames();
            args.Add("uids", uids.ToList().ConvertAll(uid => uid.ToString()).ToArray());
            args.Add("fields", fields);
            var response = this.ExecuteRequest<FacebookList<User>>("Users.getInfo", args);
            return response;
        }

        /// <summary>Returns an array of user-specific information for use by the application itself.</summary>
        /// <param name="uids">List of user IDs. This is a comma-separated list of user IDs.</param>
        /// <param name="fields">List of desired fields in return. This is a comma-separated list of field strings and is limited to these entries only: <code>uid</code>, <code>first_name</code>, <code>last_name</code>, <code>name</code>, <code>timezone</code>, <code>birthday</code>, <code>sex</code>, <code>affiliations</code> (regional type only), <code>locale</code>, <code>profile_url</code>, <code>proxied_email</code>.</param>
        public FacebookResponse<FacebookList<User>> GetStandardInfo(UserProperty propertySet, params Object[] uids)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            var fields = propertySet.ToFieldNames();
            args.Add("uids", uids.ToList().ConvertAll(uid => uid.ToString()).ToArray());
            args.Add("fields", fields);
            var response = this.ExecuteRequest<FacebookList<User>>("Users.getStandardInfo", args);
            return response;
        }
    }
}