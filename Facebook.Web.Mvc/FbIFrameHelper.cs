using System;
using System.Linq;
using System.Text;
using System.Web;

namespace Facebook.Web.Mvc
{
    public class FbIFrameHelper
    {
        public FbIFrameHelper(IFacebookHttpAccessor accessor)
        {
            this.Accessor = accessor;
        }

        private IFacebookHttpAccessor Accessor { get; set; }

        public String RequireSessionScript(String path)
        {
            path = path.Split('?')[0];
            var script = new StringBuilder();
            script.AppendLine("var session = FB.Facebook.apiClient.get_session();");
            script.AppendLine(String.Format("var url = '{0}' + '?fb_sig_added=1'", path));
            script.AppendLine("for(var key in session) url += '&'+'fb_sig_'+key+'='+session[key];");
            script.AppendLine("window.location.href = url;");
            return script.ToString();
        }

        private String ActionLink(String text, String path, Boolean requireSession, String extendedPermission)
        {
            var link = new StringBuilder(String.Format("<a href=\"{0}\"", path));
            link.AppendFormat(path);

            if (requireSession)
            {
                var url = path + (path.IndexOf('?') < 0 ? "?" : "&") + "fb_sig_added=1";
                var redirectCallback = String.Format("function(){{{0}}}", this.RequireSessionScript(path));
                String callback = null;
                if (!String.IsNullOrEmpty(extendedPermission)) callback = String.Format("function(){{FB.Connect.showPermissionDialog('{0}',{1});}}", extendedPermission, redirectCallback);
                else callback = redirectCallback;

                link.AppendFormat(" onclick=\"FB.Connect.requireSession({0});return false;\"", callback);
            }
            link.AppendFormat(">{0}</a>", text);
            return link.ToString();
        }

        public String ActionLink(String text, String path)
        {
            return this.ActionLink(text, path, false, null);
        }

        public String ActionLink(String text, String path, Boolean requireSession)
        {
            return this.ActionLink(text, path, requireSession, null);
        }

        public String ActionLink(String text, String path, String extendedPermission)
        {
            return this.ActionLink(text, path, true, extendedPermission);
        }
    }
}