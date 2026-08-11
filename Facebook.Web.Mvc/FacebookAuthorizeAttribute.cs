using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Facebook.Web.Security;

namespace Facebook.Web.Mvc
{
    /// <summary>An <see cref="IAuthorizationFilter" /> implementation based off Facebook's authorization and extended permissions systems.</summary>
    /// <remarks>
    /// The presence of the <see cref="FacebookAuthorizeAttribute" /> on a controller or action will require the user's authorization of the application.
    /// Specifying a value for the <see cref="Required" /> property will also require those extended permissions for the user to be able to access that action.
    /// Specifying a value for the <see cref="Optional" /> property will prompt a user to grant the permissions once. If they decline a cookie will be set, with the 
    /// duration of the cookie controlled by the <see cref="OptionalPermissionsReminderFrequency" /> property. The user will not be prompted for that permission again
    /// until the cookie expires.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class FacebookAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public static readonly ExtendedPermission NonRequestablePermissions = PopulateNonRequestablePermissions();

        private static ExtendedPermission PopulateNonRequestablePermissions()
        {
            var type = typeof(ExtendedPermission);
            var fields = type.GetFields();
            var result =
                from extendedPermission in Enum.GetValues(type).OfType<ExtendedPermission>()
                join field in fields on extendedPermission.ToString() equals field.Name                
                where !field.GetAttribute<PermissionAttribute>().CanBeRequested
                select extendedPermission;

            var nonRequestable = default(ExtendedPermission);
            foreach (var extendedPermission in result)
            {
                nonRequestable |= extendedPermission;
            }

            return nonRequestable;
        }

        public FacebookAuthorizeAttribute() { }

        public FacebookAuthorizeAttribute(ExtendedPermission required)
            : this(required, default(ExtendedPermission))
        {
            this.OptionalPermissionsReminderFrequency = DeclinedPermissionsCookie.DefaultDuration;
        }

        public FacebookAuthorizeAttribute(ExtendedPermission required, ExtendedPermission optional)
        {
            this.Required = required;
            this.Optional = optional;

            if ((this.Required & this.Optional) != default(ExtendedPermission))
            {
                throw new ArgumentException("Required and Optional permissions must specify unique sets of permission.", "optional");
            }
        }

        public ExtendedPermission Required { get; set; }

        public ExtendedPermission Optional { get; set; }

        public DeclinedPermissionsCookieDuration OptionalPermissionsReminderFrequency { get; set; }

        protected override Boolean AuthorizeCore(HttpContextBase httpContext)
        {
            var context = FacebookHttpContext.GetCurrent(httpContext.Items);
            String apiKey = context.ApiKey;

            if (((this.Required | this.Optional) & NonRequestablePermissions) != default(ExtendedPermission))
            {
                throw new FacebookException("The extended permission(s) {0} cannot be requested.", ((this.Required | this.Optional) & NonRequestablePermissions));
            }

            if (context.HasSession)
            {
                // if the user has already declined optional permissions, ignore them so they do not get re-requested
                var declinedPermissions = httpContext.GetDeclinedPermissions(context.ApiKey, context.Session.Uid);

                // this assigns the result of the binary anding of the binary compliment of the declinedPermissions to the Optional property.
                // it has the effect of removing any declined permissions from the Optional property, while keeping permissions that haven't been declined.
                this.Optional &= ~declinedPermissions;

                // if the user has already granted all the required and remaining optional permissions, nothing left to do here
                if ((context.Session.ExtendedPermissions & this.Required) == this.Required
                   && (context.Session.ExtendedPermissions & this.Optional) == this.Optional) return true;
            }

            return false;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!this.AuthorizeCore(filterContext.HttpContext))
            {
                var context = FacebookHttpContext.GetCurrent(filterContext.HttpContext.Items);

                var uri = filterContext.HttpContext.Request.Url;
                var baseUri = new UriBuilder(uri);
                baseUri.Path = String.Empty;

                var urlHelper = new UrlHelper(filterContext.RequestContext);
                var redirect = new UriBuilder(new Uri(baseUri.Uri, urlHelper.Content(Facebook.Configuration.FacebookSectionGroup.Api.AuthUrl)));

                var queryBuilder = new StringBuilder(redirect.Query);
                if (queryBuilder.Length == 0 || !queryBuilder.ToString().EndsWith("&")) queryBuilder.Append("&");
                queryBuilder.Append("returnUrl=");
                queryBuilder.Append(HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString()));
                queryBuilder.Append("&required=");
                // this ensures that we only request the permissions that are missing (see note "existing permissions" below)
                queryBuilder.Append((Int64)(context.Session.ExtendedPermissions ^ this.Required));
                queryBuilder.Append("&optional=");
                queryBuilder.Append((Int64)(context.Session.ExtendedPermissions ^ this.Optional));
                queryBuilder.Append("&duration=");
                queryBuilder.Append(this.OptionalPermissionsReminderFrequency);
                redirect.Query = queryBuilder.ToString();

                filterContext.HttpContext.Response.Redirect(redirect.ToString());

                // existing permissions:    the last time I checked, Facebook still showed the prompt for permissions that were already granted
                //                          when prompting the user for permissions. it goes away automatically, but still slows the user
                //                          experience down.
            }
        }
    }
}