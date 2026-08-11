using System;
using System.CodeDom;

namespace Facebook.Api.Generator
{
    public class CompileItem
    {
        public CodeCompileUnit CompileUnit { get; set; }
        public String NamespacePath { get; set; }
        public String FileName { get; set; }

        public override String ToString()
        {
            return this.NamespacePath + "\\" + this.FileName;
        }
    }
}