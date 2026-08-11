using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Facebook.VisualStudio
{
    public partial class FacebookInfoInput : Form
    {
        public FacebookInfoInput()
        {
            InitializeComponent();
        }

        public String ApiKey
        {
            get { return apiKeyBox.Text; }
        }

        public String Secret
        {
            get { return secretBox.Text; }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(apiKeyBox.Text) || String.IsNullOrEmpty(secretBox.Text))
            {
                errorLabel.Visible = true;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void createAppLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start((String)((Control)sender).Tag);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start((String)((Control)sender).Tag);
        }
    }
}
