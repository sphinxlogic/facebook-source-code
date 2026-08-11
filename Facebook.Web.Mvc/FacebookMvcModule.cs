using System;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Web.Mvc
{
    public class FacebookMvcModule : FacebookHttpModule
    {
        public override void Init(HttpApplication application)
        {
            base.Init(application);

            ControllerBuilder.Current.DefaultNamespaces.Add("Facebook.Web.Mvc");
        }   
    }
}