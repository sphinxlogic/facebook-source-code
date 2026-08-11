using System;
using System.Collections;
using System.Configuration;
using Facebook;
using Facebook.Web;
using Facebook.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace NameSpace.WebFormsTemplate
{
    public partial class _Default : Facebook.Web.UI.FacebookPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void RevokeAuthorization()
        {
            this.FbContext.Auth.RevokeAuthorization(this.FbSession.Uid);
            this.FbSession.Added = false;
            this.FbContext.Session.Expires = DateTime.Now;
        }
    }
}
