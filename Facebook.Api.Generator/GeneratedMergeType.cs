using System;

namespace Facebook.Api.Generator
{
    public enum GeneratedMergeType : byte
    {
        /// <summary>The type is manually hard-coded and should not be generated.</summary>
        DoNotGenerate = 0,

        /// <summary>The type is been partially hard-coded and should be complemented by a generated partial class.</summary>
        GeneratePartial = 1,

        /// <summary>The type has not been hard-coded and is represented by its generated type only.</summary>
        GeneratedOnly = 2
    }
}