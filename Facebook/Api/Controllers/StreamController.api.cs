using System;
using System.Collections.Generic;

namespace Facebook.Api.Controllers
{
    public partial class StreamController : FacebookApiController
    {
        /// <summary>This method publishes a post into the stream on the user's Wall and News Feed. This post also appears in the user's friends' streams (their News Feeds).</summary>
        /// <param name="message">The message the user enters for the post at the time of publication.</param>
        public FacebookResponse<String> Publish(String message, List<ActionLink> actionLinks)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("message", message);
            args.Add("action_links", actionLinks);
            var response = this.ExecuteRequest<String>("Stream.publish", args);
            return response;
        }

        /// <summary>This method publishes a post into the stream on the user's Wall and News Feed. This post also appears in the user's friends' streams (their News Feeds).</summary>
        /// <param name="item">A <see cref="StreamPublishItem" /> object that contains all the data necessary for the <c>Stream.Publish</c> API call.</param>
        public FacebookResponse<String> Publish(StreamPublishItem item)
        {
            Dictionary<String, Object> args = new Dictionary<String, Object>();
            args.Add("message", item);
            var response = this.ExecuteRequest<String>("Stream.publish", args);
            return response;
        }
    }
}