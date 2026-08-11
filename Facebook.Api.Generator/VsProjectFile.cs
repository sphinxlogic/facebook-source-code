using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Facebook.Api.Generator
{
    public class VsProjectFile : XDocument
    {
        public VsProjectFile(String path) : base(XDocument.Load(path))
        {
            this.FilePath = path;
            this.AddQueue = new List<XElement[]>();
        }

        public String FilePath { get; set; }

        public void Save()
        {
            foreach (var queueItem in this.AddQueue)
            {
                queueItem[0].Add(queueItem[1]);
            }

            this.Save(this.FilePath);
        }

        public XName Name(String localName)
        {
            return XName.Get(localName, this.Root.GetDefaultNamespace().NamespaceName);
        }

        public void RemoveGeneratedFileReferences()
        {
            var compileElements = this.Descendants(this.Name("Compile"));

            var generatedCompileElements = (
                from compile in compileElements
                where compile.Attribute("Include") != null && compile.Attribute("Include").Value.Contains(".g.")
                select compile).ToList();

            var removed = 0;
            for (var index = 0; index < generatedCompileElements.Count; index++)
            {
                var compile = generatedCompileElements[index];
                compile.Remove();
                removed++;
            }
        }

        public List<XElement[]> AddQueue { get; private set; }

        public void AddFileReference(String relativePath, String dependentFile)
        {
            var group = (
                from itemGroup in this.Descendants(this.Name("ItemGroup"))
                where itemGroup.Descendants(this.Name("Compile")).Count() > 0
                select itemGroup).First();

            var compile = new XElement(this.Name("Compile"), new XAttribute("Include", relativePath));
            if (!String.IsNullOrEmpty(dependentFile))
            {
                compile.Add(new XElement(this.Name("DependentUpon"), dependentFile));
            }

            this.AddQueue.Add(new XElement[] { group, compile });
        }

        public void AddFileReference(String relativePath)
        {
            this.AddFileReference(relativePath, null);
        }

        public GeneratedMergeType GetMergeType(String pathWithNoExtension, out String dependentFile)
        {
            var hardCoded = (
                from compile in this.Descendants(this.Name("Compile"))
                where compile.Attribute("Include").Value.StartsWith(pathWithNoExtension + ".")
                select compile.Attribute("Include").Value).FirstOrDefault();

            if (hardCoded == null)
            {
                dependentFile = null;
                return GeneratedMergeType.GeneratedOnly;
            }
            else
            {
                dependentFile = hardCoded.Split('\\').Last();
                if (hardCoded.Contains(".api.")) return GeneratedMergeType.GeneratePartial;
                else return GeneratedMergeType.DoNotGenerate;
            }
        }
    }
}