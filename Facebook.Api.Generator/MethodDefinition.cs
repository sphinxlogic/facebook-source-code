using System;
using System.CodeDom;

namespace Facebook.Api.Generator
{
    public class MethodDefinition
    {
        public MethodDefinition()
        {
            this.ApplicationType = ApplicationType.Website | ApplicationType.Desktop;
        }

        public String Name { get; set; }
        public String Description { get; set; }
        public MethodParameterDefinition[] Parameters { get; set; }
        public ApplicationType ApplicationType { get; set; }
    }
}