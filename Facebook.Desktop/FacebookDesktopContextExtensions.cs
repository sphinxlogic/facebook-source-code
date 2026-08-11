using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Api;
using System.Windows.Forms;

namespace Facebook.Desktop
{
    public static class FacebookDesktopContextExtensions
    {
        private const Int32 MAX_RETRY_COUNT = 5;

        public static SessionInfo EstablishSession(this FacebookDesktopContext context)
        {
            return EstablishSessionInternal(context, null, 0);
        }        

        private static SessionInfo EstablishSessionInternal(FacebookDesktopContext context, IWin32Window owner, Int32 retryCount)
        {
            var authToken = context.Auth.CreateToken();
            var authForm = new AuthForm();

            authForm.NavigateToLoginUrl(context.ApiKey, authToken);
            var result = authForm.ShowDialog(owner);

            var getSessionResult = context.Auth.GetSession(authToken);
            if (getSessionResult.IsError)
            {
                if (retryCount < MAX_RETRY_COUNT)
                {
                    var errorResult = MessageBox.Show(owner, getSessionResult.ResponseException.Message, "Error authenticating Facebook Session", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (errorResult == DialogResult.Retry && retryCount < MAX_RETRY_COUNT)
                    {
                        retryCount++;
                        return EstablishSessionInternal(context, owner, retryCount);
                    }
                }                
                return null;
            }
            else
            {
                context.InitSession(getSessionResult.Value);
                return getSessionResult.Value;
            }
        }

        public static SessionInfo EstablishSession(this FacebookDesktopContext context, IWin32Window owner)
        {
            return EstablishSessionInternal(context, owner, 0);
        }

        public static Boolean RequestExtendedPermission(this FacebookDesktopContext context, String extendedPermission)
        {
            return RequestExtendedPermissionInternal(context, null, extendedPermission, 0);
        }

        public static Boolean RequestExtendedPermission(this FacebookDesktopContext context, IWin32Window owner, String extendedPermission)
        {
            return RequestExtendedPermissionInternal(context, owner, extendedPermission, 0);
        }

        private static Boolean RequestExtendedPermissionInternal(FacebookDesktopContext context, IWin32Window owner, String extendedPermission, Int32 retryCount)
        {
            var authForm = new AuthForm();

            authForm.NavigateToAuthorizeUrl(context.ApiKey, extendedPermission);
            var result = authForm.ShowDialog(owner);

            var getSessionResult = context.Users.HasAppPermission(extendedPermission);
            if (getSessionResult.IsError)
            {
                if (retryCount < MAX_RETRY_COUNT)
                {
                    var errorResult = MessageBox.Show(owner, getSessionResult.ResponseException.Message, "Error authenticating Facebook Session", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (errorResult == DialogResult.Retry && retryCount < MAX_RETRY_COUNT)
                    {
                        retryCount++;
                        return RequestExtendedPermissionInternal(context, owner, extendedPermission, retryCount);
                    }
                }
                return false;
            }
            else
            {
                return getSessionResult.Value;
            }
        }
    }
}