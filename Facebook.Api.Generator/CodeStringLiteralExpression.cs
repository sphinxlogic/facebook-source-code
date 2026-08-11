using System;
using System.CodeDom;

namespace Facebook.Api.Generator
{
    /// <summary>Represents a string literal expression.</summary>
    public class CodeStringLiteralExpression : CodeSnippetExpression
    {
        /// <summary>Initializes an instance of the <see cref="CodeStringLiteralExpression" /> class using the specified <paramref name="literal" />.</summary>
        /// <param name="literal">The string literal expression to represent.</param>
        public CodeStringLiteralExpression(String literal)
            : base("\"" + literal + "\"") { }
    }
}