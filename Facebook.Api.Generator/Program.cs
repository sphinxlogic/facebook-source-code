using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Facebook.Api.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            FacebookApiGenerator.Generate("C#", Path.Combine(Environment.CurrentDirectory, @"..\..\..\Facebook\"));
        }
    }
}
