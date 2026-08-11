using System;
using System.Web.Mvc;
using Facebook.Desktop;
using Facebook.Sites.Desktop.Auth.Models;
using Facebook.Web.Configuration;

namespace Facebook.Sites.Desktop.Auth.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
            this.TempDataProvider = new NoTempDataProvider();
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult CreateToken(String api_key)
        {
            if (Configuration.FacebookSectionGroup.HasApiSection)
            {
                var app = Configuration.FacebookSectionGroup.Api.Applications.GetByApiKey(api_key);
                if (app == null) throw new FacebookException("A configuration for the specified application could not be found.");
                else
                {
                    var context = new FacebookDesktopContext(app.ApiKey, app.Secret) { Timeout = 15000 };
                    var createTokenResult = context.Auth.CreateToken();
                    if (createTokenResult.ResponseContent != null)
                    {
                        return new XmlResult(createTokenResult.ResponseContent);
                    }
                    else throw createTokenResult.ResponseException;
                }
            }
            else throw new FacebookException("There is no Facebook configuration for this application.");
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult GetSession(String api_key, String auth_token)
        {
            if (Configuration.FacebookSectionGroup.HasApiSection)
            {
                var app = Configuration.FacebookSectionGroup.Api.Applications.GetByApiKey(api_key);
                if (app == null) throw new FacebookException("A configuration for the specified application could not be found.");
                else
                {
                    var context = new FacebookDesktopContext(app.ApiKey, app.Secret) { Timeout = 15000 };
                    var getSessionResult = context.Auth.GetSession(auth_token);
                    if (getSessionResult.ResponseContent != null)
                    {
                        return new XmlResult(getSessionResult.ResponseContent);
                    }
                    else throw getSessionResult.ResponseException;
                }
            }
            else throw new FacebookException("There is no Facebook configuration for this application.");
        }
    }
}