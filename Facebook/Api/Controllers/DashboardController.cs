using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Facebook.Api.Controllers {
    
    
    public partial class DashboardController : FacebookApiController {
        
        public DashboardController(IFacebookContext facebookContext) : 
                base(facebookContext) {
        }
        
        /// <summary></summary>
        /// <param name="news">A JSON-encoded array containing between 1 and 20 news item objects. Each news item object is an array that contains the following: 
        ///<ul><li> <code>message</code> (required): This is a text-only string containing the content of the activity. You can also mention users in the message. Use this syntax: @:USER_ID. When the news is published, Facebook replaces the user ID with that user's full name. 
        ///</li></ul>
        ///<p><b>Note:</b> The message gets truncated after 50 characters, including any user names referenced.
        ///</p>
        ///<ul><li> <code>action_link</code>: An associative array containing an action link text and URL. The action link appears after the message and follows the same <a href="/index.php/Action_Link_Guidelines" title="Action Link Guidelines">guidelines</a> as action links in stream stories.
        ///<ul><li> <code>text</code>: The text of the action link, which gets truncated after 25 characters. 
        ///</li><li> <code>href</code>: The URL of the action link.</param>
        public FacebookResponse<Int64> AddGlobalNews(params DashboardItem[] news)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("news", news);
            var response = this.ExecuteRequest<Int64>("Dashboard.addGlobalNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="image">The absolute URL to the image associated with the array of news items. Facebook formats the image as a 64x64 pixel square. This image cannot be a Facebook.com URL. 
        ///If you don't specify an image, Facebook displays your application logo instead.</param>
        /// <param name="news">A JSON-encoded array containing between 1 and 20 news item objects. Each news item object is an array that contains the following: 
        ///<ul><li> <code>message</code> (required): This is a text-only string containing the content of the activity. You can also mention users in the message. Use this syntax: @:USER_ID. When the news is published, Facebook replaces the user ID with that user's full name. 
        ///</li></ul>
        ///<p><b>Note:</b> The message gets truncated after 50 characters, including any user names referenced.
        ///</p>
        ///<ul><li> <code>action_link</code>: An associative array containing an action link text and URL. The action link appears after the message and follows the same <a href="/index.php/Action_Link_Guidelines" title="Action Link Guidelines">guidelines</a> as action links in stream stories.
        ///<ul><li> <code>text</code>: The text of the action link, which gets truncated after 25 characters. 
        ///</li><li> <code>href</code>: The URL of the action link.</param>
        public FacebookResponse<Int64> AddGlobalNews(String image, params DashboardItem[] news)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("image", image);
            args.Add("news", news);
            var response = this.ExecuteRequest<Int64>("Dashboard.addGlobalNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="uid">The ID of the user whose dashboard you are updating. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        /// <param name="news">A JSON-encoded array containing between 1 and 20 news item objects. Each news item object is an array that contains the following:  
        ///<ul><li> <code>message</code> (required): This is a text-only string containing the content of the activity. You can include one {*actor*} token, which is the same token used in <a href="/index.php/Attachment_%28Streams%29" title="Attachment (Streams)">stream attachments</a>. When the news is published, Facebook replaces this token with the full name of the current session user or the user associated with the user ID passed in <code>uid</code>. 
        ///</li></ul>
        ///<p>You can also mention other users in the message. Use this syntax: @:USER_ID. When the news is published, Facebook replaces the user ID with that user's full name. 
        ///</p><p><b>Note:</b> The message gets truncated after 50 characters, including any user names referenced.
        ///</p>
        ///<ul><li> <code>action_link</code>: An associative array containing an action link text and URL. The action link appears after the message and follows the same <a href="/index.php/Action_Link_Guidelines" title="Action Link Guidelines">guidelines</a> as action links in stream stories.
        ///<ul><li> <code>text</code>: The text of the action link, which gets truncated after 25 characters. 
        ///</li><li> <code>href</code>: The URL of the action link.</param>
        public FacebookResponse<Int64> AddNews(Int64 uid, params DashboardItem[] news)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            args.Add("news", new FacebookAttributeBasedList<DashboardItem>(news));
            var response = this.ExecuteRequest<Int64>("Dashboard.addNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="image">The absolute URL to the image associated with the news item. Facebook formats the image as a 64x64 pixel square. This image cannot be a Facebook.com URL.
        ///If you don't specify an image, Facebook displays your application logo instead.</param>
        /// <param name="uid">The ID of the user whose dashboard you are updating. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        /// <param name="news">A JSON-encoded array containing between 1 and 20 news item objects. Each news item object is an array that contains the following:  
        ///<ul><li> <code>message</code> (required): This is a text-only string containing the content of the activity. You can include one {*actor*} token, which is the same token used in <a href="/index.php/Attachment_%28Streams%29" title="Attachment (Streams)">stream attachments</a>. When the news is published, Facebook replaces this token with the full name of the current session user or the user associated with the user ID passed in <code>uid</code>. 
        ///</li></ul>
        ///<p>You can also mention other users in the message. Use this syntax: @:USER_ID. When the news is published, Facebook replaces the user ID with that user's full name. 
        ///</p><p><b>Note:</b> The message gets truncated after 50 characters, including any user names referenced.
        ///</p>
        ///<ul><li> <code>action_link</code>: An associative array containing an action link text and URL. The action link appears after the message and follows the same <a href="/index.php/Action_Link_Guidelines" title="Action Link Guidelines">guidelines</a> as action links in stream stories.
        ///<ul><li> <code>text</code>: The text of the action link, which gets truncated after 25 characters. 
        ///</li><li> <code>href</code>: The URL of the action link.</param>
        public FacebookResponse<Int64> AddNews(String image, Int64 uid, params DashboardItem[] news)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("image", image);
            args.Add("uid", uid);
            args.Add("news", news);
            var response = this.ExecuteRequest<Int64>("Dashboard.addNews", args);
            return response;
        }
        
        /// <summary></summary>
        public FacebookResponse<Boolean> ClearGlobalNews() {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            var response = this.ExecuteRequest<Boolean>("Dashboard.clearGlobalNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="newsIds">An array of news_ids to clear. If you do not specify any news_ids, then all global news gets cleared for your application.</param>
        public FacebookResponse<Boolean> ClearGlobalNews(params Int64[] newsIds) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("news_ids", newsIds);
            var response = this.ExecuteRequest<Boolean>("Dashboard.clearGlobalNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="newsIds">An array of news_ids to clear. If you do not pass this parameter or specify any news_ids, then all personal news gets cleared for the specified user.</param>
        /// <param name="uid">The ID of the user whose dashboard you are updating. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        public FacebookResponse<Boolean> ClearNews(Int64 uid, params Int64[] newsIds) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("news_ids", newsIds);
            args.Add("uid", uid);
            var response = this.ExecuteRequest<Boolean>("Dashboard.clearNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="uid">The ID of the user whose counter you are decrementing. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        public FacebookResponse<Boolean> DecrementCount(Int64 uid) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            var response = this.ExecuteRequest<Boolean>("Dashboard.decrementCount", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="activityIds">An array of activity_ids to return. If you do not specify any activity_ids, then all activities get returned for the specified or session user.</param>
        public FacebookResponse<FacebookAttributeBasedList<DashboardActivity>> GetActivity(params Int64[] activityIds)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("activity_ids", activityIds);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<DashboardActivity>>("Dashboard.getActivity", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="uid">The ID of the user whose counter you are retrieving. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        public FacebookResponse<Int64> GetCount(Int64 uid) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            var response = this.ExecuteRequest<Int64>("Dashboard.getCount", args);
            return response;
        }
        
        /// <summary></summary>
        public FacebookResponse<FacebookAttributeBasedList<GlobalNewsItem>> GetGlobalNews()
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            var response = this.ExecuteRequest<FacebookAttributeBasedList<GlobalNewsItem>>("Dashboard.getGlobalNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="newsIds">An array of news_ids to return. If you do not specify any news_ids, then all global news gets returned.</param>
        public FacebookResponse<FacebookAttributeBasedList<GlobalNewsItem>> GetGlobalNews(String[] newsIds)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("news_ids", newsIds);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<GlobalNewsItem>>("Dashboard.getGlobalNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="uid">The ID of the user whose dashboard you are updating. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        public FacebookResponse<FacebookAttributeBasedList<NewsItem>> GetNews(Int64 uid)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<NewsItem>>("Dashboard.getNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="newsIds">An array of news_ids to return. If you do not specify any news_ids, then all personal news gets returned for the specified user.</param>
        /// <param name="uid">The ID of the user whose dashboard you are updating. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        public FacebookResponse<FacebookAttributeBasedList<NewsItem>> GetNews(String[] newsIds, Int64 uid)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("news_ids", newsIds);
            args.Add("uid", uid);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<NewsItem>>("Dashboard.getNews", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="uid">The ID of the user whose counter you are incrementing. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        public FacebookResponse<Boolean> IncrementCount(Int64 uid) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            var response = this.ExecuteRequest<Boolean>("Dashboard.incrementCount", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<MultiAddNewsResult>> MultiAddNews(DashboardItem[] news, params Int64[] uids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("uids", uids);
            args.Add("news", news);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<MultiAddNewsResult>>("Dashboard.multiAddNews", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<MultiAddNewsResult>> MultiAddNews(String imageUrl, DashboardItem[] news, params Int64[] uids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("image", imageUrl);
            args.Add("uids", uids);
            args.Add("news", news);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<MultiAddNewsResult>>("Dashboard.multiAddNews", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<MultiClearNewsResult>> MultiClearNews(params MultiNewsRequestItem[] ids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("ids", ids.ToDictionary(id => id.Uid, id => id.NewsIds));
            var response = this.ExecuteRequest<FacebookAttributeBasedList<MultiClearNewsResult>>("Dashboard.multiClearNews", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<MultiNews>> MultiGetNews(params MultiNewsRequestItem[] ids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("ids", ids.ToDictionary(id => id.Uid, id => id.NewsIds));
            var response = this.ExecuteRequest<FacebookAttributeBasedList<MultiNews>>("Dashboard.multiGetNews", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<DashboardMultiIncrementCountResult>> MultiIncrementCount(params Int64[] uids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("uids", uids);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<DashboardMultiIncrementCountResult>>("Dashboard.multiIncrementCount", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<DashboardMultiDecrementCountResult>> MultiDecrementCount(params Int64[] uids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("uids", uids);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<DashboardMultiDecrementCountResult>>("Dashboard.multiDecrementCount", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<DashboardMultiSetCountResult>> MultiSetCount(params DashboardMultiSetCountItem[] ids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("ids", ids.ToDictionary(i => i.Uid, i => i.Count));
            var response = this.ExecuteRequest<FacebookAttributeBasedList<DashboardMultiSetCountResult>>("Dashboard.multiSetCount", args);
            return response;
        }

        public FacebookResponse<FacebookAttributeBasedList<DashboardMultiGetCountResult>> MultiGetCount(params Int64[] uids)
        {
            var args = new Dictionary<String, Object>();
            args.Add("uids", uids);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<DashboardMultiGetCountResult>>("Dashboard.multiGetCount", args);
            return response;
        }

        /// <summary></summary>
        /// <param name="activity">A JSON-encoded array containing between 1 and 20 activity objects. Each activity object is an array that contains the following:  
        ///<ul><li> <code>message</code> (required): This is a text-only string containing the content of the activity. The message <b>must</b> also contain the {*actor*} token, which is the same token used in <a href="/index.php/Attachment_%28Streams%29" title="Attachment (Streams)">stream attachments</a>. When the activity is published, Facebook replaces this token with the full name of the user whose session key you're using to made the call. 
        ///</li></ul>
        ///<p>You can also mention other users in the message. Use this syntax: @:USER_ID. When the activity is published, Facebook replaces the user ID with that user's full name. 
        ///</p><p><b>Note:</b> The message gets truncated after 50 characters, including any user names referenced.
        ///</p>
        ///<ul><li> <code>action_link</code>: An associative array containing one action link's text and URL. The action link appears after the message and follows the same <a href="/index.php/Action_Link_Guidelines" title="Action Link Guidelines">guidelines</a> as action links in stream stories.
        ///<ul><li> <code>text</code>: The text of the action link, which gets truncated after 25 characters. 
        ///</li><li> <code>href</code>: The URL of the action link.</param>
        public FacebookResponse<Int64> PublishActivity(DashboardActivityPublishItem activity)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("activity", activity);
            var response = this.ExecuteRequest<Int64>("Dashboard.publishActivity", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="activityIds">An array of activity_ids to clear. You must include at least one activity_id in the array; you cannot pass an empty array.</param>
        public FacebookResponse<FacebookAttributeBasedList<RemoveActivityResult>> RemoveActivity(params Int64[] activityIds)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("activity_ids", activityIds);
            var response = this.ExecuteRequest<FacebookAttributeBasedList<RemoveActivityResult>>("Dashboard.removeActivity", args);
            return response;
        }
        
        /// <summary></summary>
        /// <param name="uid">The ID of the user whose counter you are setting. If you include the uid, you must sign your call with your application secret. If you do not include the uid, you must include a session key and sign your call with a <a href="/index.php/Session_Secret" title="Session Secret">session secret</a>.</param>
        /// <param name="count">The new value of the counter. The value must be an integer 0 or greater.</param>
        public FacebookResponse<Boolean> SetCount(Int64 uid, Int64 count) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            args.Add("count", count);
            var response = this.ExecuteRequest<Boolean>("Dashboard.setCount", args);
            return response;
        }
    }
}
