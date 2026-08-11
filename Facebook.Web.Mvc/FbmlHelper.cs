using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Facebook.Web.Mvc
{
    public class FbmlHelper
    {
        public String FeedForm(String templateName, String buttonLabel, params String[] fieldNames)
        {
            var formBuilder = new StringBuilder();
            var urlBuilder = new StringBuilder();

            var url = HttpContext.Current.Request.Url;
            urlBuilder.Append(url.Scheme);
            urlBuilder.Append("://");
            urlBuilder.Append(url.Host);
            if (!url.IsDefaultPort) urlBuilder.AppendFormat(":{0}", url.Port);
            urlBuilder.Append("/Feed/PostFbml/");
            urlBuilder.Append(templateName);

            formBuilder.AppendLine("<form fbtype=\"feedStory\" action=\"" + urlBuilder.ToString() + "\">");
            foreach (var fieldName in fieldNames)
            {
                formBuilder.AppendLine("<input type=\"text\" name=\"" + fieldName + "\" />");
            }
            formBuilder.AppendLine("<input type=\"submit\" value=\"" + buttonLabel + "\" label=\"" + buttonLabel + "\" />");
            formBuilder.AppendLine("</form>");            

            return formBuilder.ToString();
        }
    }
}