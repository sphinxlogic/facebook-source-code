using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facebook.Api
{
    [Serializable]
    public class StreamPublishItem : FacebookArgs
    {
        public String Message
        {
            get { return (String)this["message"]; }
            set { this["message"] = value; }
        }

        public AttachmentData Attachment
        {
            get
            {
                if (this["attachment"] == null) this["attachment"] = new AttachmentData();
                return (AttachmentData)this["attachment"];
            }
            set { this["attachment"] = value; }
        }

        public List<ActionLink> ActionLinks
        {
            get { return (List<ActionLink>)this["action_links"]; }
            set { this["action_links"] = value; }
        }

        protected override void SerializeProperty(String key, ref IDictionary<String, Object> args)
        {
            switch (key)
            {
                case "attachment":
                    {
                        var attachment = new Dictionary<String, Object>();
                        if (this.Attachment.Name != null) attachment.Add("name", this.Attachment.Name);
                        if (this.Attachment.Href != null) attachment.Add("href", this.Attachment.Href);
                        if (this.Attachment.Caption != null) attachment.Add("caption", this.Attachment.Caption);
                        if (this.Attachment.Description != null) attachment.Add("description", this.Attachment.Description);
                        if (this.Attachment.Properties != null) attachment.Add("properties", this.Attachment.Properties);
                        if (this.Attachment.Media != null) attachment.Add("media", this.Attachment.Media);
                        if (this.Attachment.CommentsXid != null) attachment.Add("comments_xid", this.Attachment.CommentsXid);
                        args["attachment"] = attachment;
                    }
                    break;
            }
        }

        public class AttachmentData
        {
            public String Name { get; set; }
            public String Href { get; set; }
            public String Caption { get; set; }
            public String Description { get; set; }
            public List<Dictionary<String, Object>> Properties { get; set; }
            public List<Dictionary<String, Object>> Media { get; set; }
            public String CommentsXid { get; set; }
        }
    }
}