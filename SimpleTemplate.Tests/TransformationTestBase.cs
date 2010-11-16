namespace SimpleTemplate.Tests
{
    using System.IO;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    public abstract class TransformationTestBase
    {
        protected Template template;
        protected string templateFile;

        [SetUp]
        public void Setup()
        {
            this.templateFile = Path.GetTempFileName();
            this.template = new Template(this.templateFile);
        }

        [TearDown]
        public void Teardown()
        {
            FileSystemUtil.DeleteIfExist(this.template.TemplateFile);
            FileSystemUtil.DeleteIfExist(this.template.TransformationFile);
        }

        protected void AssertFile(string generatedFileName, string result)
        {
            using (var reader = new StreamReader(File.OpenRead(generatedFileName)))
            {
                reader.ReadToEnd().ShouldEqual(result);
            }
        }

        protected void CreateTemplateFile(string templateText, string templateFileName)
        {
            using (var writer = new StreamWriter(File.Create(templateFileName)))
            {
                writer.Write(templateText);
            }
        }
    }
}