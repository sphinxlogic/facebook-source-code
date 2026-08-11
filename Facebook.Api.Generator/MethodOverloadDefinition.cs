using System;

namespace Facebook.Api.Generator
{
    public class MethodOverloadDefinition
    {
        public String Suffix { get; set; }
        public MethodParameterDefinition[] Parameters { get; set; }
        public ApplicationType ApplicationType { get; set; }
    }
}