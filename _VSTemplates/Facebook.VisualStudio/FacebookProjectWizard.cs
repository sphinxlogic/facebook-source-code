using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;

namespace Facebook.VisualStudio
{
    public class FacebookProjectWizard : IWizard
    {
        #region [ IWizard Members ]

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(Object automationObject, Dictionary<String, String> replacementsDictionary, WizardRunKind runKind, Object[] customParams)
        {
            try
            {
                var input = new FacebookInfoInput();
                var result = input.ShowDialog();

                if (result == DialogResult.OK)
                {
                    replacementsDictionary.Add("$fb_api_key$", input.ApiKey);
                    replacementsDictionary.Add("$fb_secret$", input.Secret);
                }
                else
                {
                    replacementsDictionary.Add("$fb_api_key$", "YOUR_API_KEY_HERE");
                    replacementsDictionary.Add("$fb_secret$", "YOUR_APP_SECRET_HERE");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Boolean ShouldAddProjectItem(String filePath)
        {
            return true;
        }

        #endregion
    }
}
