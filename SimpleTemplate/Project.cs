
namespace SimpleTemplate
{
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class Project
    {
        private readonly string projectFile;

        public Project(string projectFile)
        {
            this.projectFile = projectFile;
        }
        
        public void IncludeCompileFile(string includeFile)
        {
            var xelement = this.FindCompileIncludeFile(includeFile);

            if (xelement.Count == 0)
            {
                this.AddToProjectFile(
                    this.projectFile, 
                    includeFile.Replace("..\\", string.Empty).Replace("../", string.Empty));    
            }
        }

        public XPathNodeIterator FindCompileIncludeFile(string includeFile)
        {
            var document = XDocument.Load(projectFile);

            if (document.Root != null)
            {
                var navigator = new XPathDocument(projectFile).CreateNavigator();
                var namespaceManager = new XmlNamespaceManager(navigator.NameTable);
                namespaceManager.AddNamespace("pr", "http://schemas.microsoft.com/developer/msbuild/2003");

                return navigator.Select(
                    @"pr:Project/pr:ItemGroup/pr:Compile[@Include='" + includeFile + "']",
                    namespaceManager);
            }

            return null;     
        }

        private void AddToProjectFile(string projectFileName, string projectFileEntry)
        {
            var document = XDocument.Load(projectFileName);

            if (document.Root != null)
            {
                var nameSpace = document.Root.Name.Namespace;
                var element = new XElement(
                    nameSpace + "Compile", new XAttribute("Include", projectFileEntry));
                document.Root.Elements(nameSpace + "ItemGroup").ElementAt(1).Add(element);
                document.Save(projectFileName);
            }
        }


    }
}
