using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facebook.Desktop
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        public AuthForm(String title)
        {
            this.Text = title;
        }

        public AuthForm(Icon icon)
        {
            this.Icon = icon;
        }

        public AuthForm(String text, Icon icon)
        {
            this.Text = text;
            this.Icon = icon;
        }

        public void NavigateToLoginUrl(String apiKey, String authToken)
        {
            var loginUrl = String.Format("http://www.facebook.com/login.php?api_key={0}&v=1.0&auth_token={1}", apiKey, authToken);
            this.webBrowser.Url = new Uri(loginUrl);
        }

        public void NavigateToAuthorizeUrl(String apiKey, String externalPermission)
        {
            var loginUrl = String.Format("http://www.facebook.com/authorize.php?api_key={0}&v=1.0&ext_perm={1}", apiKey, externalPermission);
            this.webBrowser.Url = new Uri(loginUrl);
        }
    }
}