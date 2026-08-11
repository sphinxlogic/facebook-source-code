namespace Facebook.VisualStudio
{
    partial class FacebookInfoInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacebookInfoInput));
            this.createAppLabel = new System.Windows.Forms.Label();
            this.directionsLabel = new System.Windows.Forms.Label();
            this.createAppLink = new System.Windows.Forms.LinkLabel();
            this.apiKeyLabel = new System.Windows.Forms.Label();
            this.appScreenShot = new System.Windows.Forms.PictureBox();
            this.apiKeyBox = new System.Windows.Forms.TextBox();
            this.secretBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.errorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.appScreenShot)).BeginInit();
            this.SuspendLayout();
            // 
            // createAppLabel
            // 
            this.createAppLabel.AutoSize = true;
            this.createAppLabel.Location = new System.Drawing.Point(12, 9);
            this.createAppLabel.Name = "createAppLabel";
            this.createAppLabel.Size = new System.Drawing.Size(383, 13);
            this.createAppLabel.TabIndex = 0;
            this.createAppLabel.Text = "If you haven\'t already created your application on Facebook, please do so here:";
            // 
            // directionsLabel
            // 
            this.directionsLabel.Location = new System.Drawing.Point(12, 48);
            this.directionsLabel.Name = "directionsLabel";
            this.directionsLabel.Size = new System.Drawing.Size(381, 47);
            this.directionsLabel.TabIndex = 1;
            this.directionsLabel.Text = "Once your app is created, fill out this form to populated your application\'s info" +
                "rmation in the configuration file.";
            // 
            // createAppLink
            // 
            this.createAppLink.AutoSize = true;
            this.createAppLink.Location = new System.Drawing.Point(12, 22);
            this.createAppLink.Name = "createAppLink";
            this.createAppLink.Size = new System.Drawing.Size(264, 13);
            this.createAppLink.TabIndex = 10;
            this.createAppLink.TabStop = true;
            this.createAppLink.Tag = "http://www.facebook.com/developers/createapp.php";
            this.createAppLink.Text = "http://www.facebook.com/developers/createapp.php";
            this.createAppLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.createAppLink_LinkClicked);
            // 
            // apiKeyLabel
            // 
            this.apiKeyLabel.AutoSize = true;
            this.apiKeyLabel.Location = new System.Drawing.Point(12, 109);
            this.apiKeyLabel.Name = "apiKeyLabel";
            this.apiKeyLabel.Size = new System.Drawing.Size(48, 13);
            this.apiKeyLabel.TabIndex = 3;
            this.apiKeyLabel.Text = "API Key:";
            // 
            // appScreenShot
            // 
            this.appScreenShot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.appScreenShot.Image = ((System.Drawing.Image)(resources.GetObject("appScreenShot.Image")));
            this.appScreenShot.Location = new System.Drawing.Point(399, 9);
            this.appScreenShot.Name = "appScreenShot";
            this.appScreenShot.Size = new System.Drawing.Size(316, 283);
            this.appScreenShot.TabIndex = 4;
            this.appScreenShot.TabStop = false;
            // 
            // apiKeyBox
            // 
            this.apiKeyBox.Location = new System.Drawing.Point(66, 106);
            this.apiKeyBox.Name = "apiKeyBox";
            this.apiKeyBox.Size = new System.Drawing.Size(327, 20);
            this.apiKeyBox.TabIndex = 0;
            // 
            // secretBox
            // 
            this.secretBox.Location = new System.Drawing.Point(66, 132);
            this.secretBox.Name = "secretBox";
            this.secretBox.Size = new System.Drawing.Size(327, 20);
            this.secretBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Secret:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(188, 43);
            this.linkLabel1.Location = new System.Drawing.Point(12, 155);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(381, 59);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "http://wiki.developers.facebook.com/index.php/How_Facebook_Authenticates_Your_App" +
                "lication";
            this.linkLabel1.Text = resources.GetString("linkLabel1.Text");
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 217);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(181, 75);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel (I will enter values myself)";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(212, 217);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(181, 75);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(12, 90);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(344, 13);
            this.errorLabel.TabIndex = 11;
            this.errorLabel.Text = "Please enter your API Key and Application Secret as shown to the right.";
            this.errorLabel.Visible = false;
            // 
            // FacebookInfoInput
            // 
            this.AcceptButton = this.continueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(719, 300);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.secretBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.apiKeyBox);
            this.Controls.Add(this.appScreenShot);
            this.Controls.Add(this.apiKeyLabel);
            this.Controls.Add(this.createAppLink);
            this.Controls.Add(this.directionsLabel);
            this.Controls.Add(this.createAppLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FacebookInfoInput";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = ".NET Facebook API Client Starter Kit - Enter Application Information";
            ((System.ComponentModel.ISupportInitialize)(this.appScreenShot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label createAppLabel;
        private System.Windows.Forms.Label directionsLabel;
        private System.Windows.Forms.LinkLabel createAppLink;
        private System.Windows.Forms.Label apiKeyLabel;
        private System.Windows.Forms.PictureBox appScreenShot;
        private System.Windows.Forms.TextBox apiKeyBox;
        private System.Windows.Forms.TextBox secretBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Label errorLabel;
    }
}