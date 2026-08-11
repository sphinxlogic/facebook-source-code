using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook.Desktop;

namespace Facebook.Apps.Demo.WinForms
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fb = FacebookDesktopContext.Current;
            fb.EstablishSession(this);

            if (fb.HasSession) MessageBox.Show("Yaaay", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Noooooo", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fb = FacebookDesktopContext.Current;
            
            var friends = fb.Friends.Get(fb.Session.Uid);
            if (friends.IsError) MessageBox.Show(this, friends.ResponseException.Message, "D'oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show(this, friends.Value.ToJson(), "Yaaay", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fb = FacebookDesktopContext.Current;
            if (fb.HasSession)
            {
                if (fb.RequestExtendedPermission("offline_access")) MessageBox.Show(this, "Request succeeded.", "Yaaaay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show(this, "Nooooo", "D'oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show(this, "Please log in first!", "Authentication required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}
