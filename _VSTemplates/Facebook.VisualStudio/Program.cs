using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.VisualStudio
{
    class Program
    {
        public static void Main()
        {
            var wiz = new FacebookProjectWizard();
            wiz.RunStarted(null, new Dictionary<string, string>(), Microsoft.VisualStudio.TemplateWizard.WizardRunKind.AsNewProject, null);            
        }
    }
}
