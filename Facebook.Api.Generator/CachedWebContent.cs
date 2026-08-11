using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Facebook;

namespace Facebook.Api.Generator
{
    /// <summary>Manages raw data that has been pulled from the web.</summary>
    public class CachedWebContent
    {
        private CachedWebContent() { }

        private CachedWebContent(String url)
        {
            this.Url = url;
            this.Filename = MD5.Create().ComputeHashString(url);
        }

        /// <summary>Gets or sets the date the document was last modified.</summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>Gets or sets the date the web version of the document was last checked for updates.</summary>
        public DateTime LastChecked { get; set; }

        /// <summary>Gets or sets the content of the document.</summary>
        public String Content { get; set; }

        /// <summary>Gets or sets the url the document was pulled from.</summary>
        public String Url { get; set; }

        /// <summary>Gets or sets the local filename for the document.</summary>
        public String Filename { get; set; }

        /// <summary>Loads the document at the specified <paramref name="url" />.</summary>
        /// <param name="url">A valid url.</param>
        /// <returns>A <see cref="CachedWebContent" /> object containing the content from the specified <paramref name="url" />.
        /// has been cached locally, that copy is returned.</returns>        
        public static CachedWebContent Load(String url)
        {
            CachedWebContent content = new CachedWebContent(url);

            // look for a locally cached copy
            if (File.Exists(content.Filename))
            {
                using (var stream = File.Open(content.Filename, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new XmlSerializer(typeof(CachedWebContent));
                    content = (CachedWebContent)serializer.Deserialize(stream);
                }
            }

            // don't bother checked again if the local copy has been checked in the last 24 hours
            if (String.IsNullOrEmpty(content.Content) || DateTime.Now - content.LastChecked > new TimeSpan(24, 0, 0))
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.IfModifiedSince = content.ModifiedOn;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.1.7) Gecko/20091221 MRA 5.5 (build 02842) Firefox/3.5.7 (.NET CLR 3.5.30729)";
                try
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            content.ModifiedOn = response.LastModified;
                            using (var reader = new StreamReader(responseStream))
                            {
                                content.Content = reader.ReadToEnd();
                            }
                        }
                    }
                }
                // receiving a 304 will throw an exception, but the local content will be returned in the event of any web exception.
                catch (WebException ex) { }
                content.LastChecked = DateTime.Now;
                content.Save();
            }
            return content;
        }

        private void Save()
        {
            using (var stream = File.Open(this.Filename, FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(CachedWebContent));
                serializer.Serialize(stream, this);
            }
        }
    }
}