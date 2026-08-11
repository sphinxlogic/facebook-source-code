using System;
using System.CodeDom;

namespace Facebook.Api.Generator
{
    public class MethodParameterDefinition
    {
        public CodeParameterDeclarationExpression DeclarationExpression { get; set; }
        public String ApiParameterName { get; set; }
        public String Description { get; set; }
    }
}