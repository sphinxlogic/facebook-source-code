using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Text;

namespace Facebook.Api.Controllers
{
    public partial class NotificationsController : FacebookApiController
    {
        public FacebookResponse<String> SendFbmlEmail(String subject, String fbml, params Int64[] recipients)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("subject", subject);
            args.Add("text", String.Empty);
            args.Add("fbml", fbml);
            args.Add("recipients", recipients.ToDelimitedString(','));
            var response = this.ExecuteRequest<String>("Notifications.sendEmail", args);
            return response;
        }
    }
}