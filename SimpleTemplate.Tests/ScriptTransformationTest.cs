namespace SimpleTemplate.Tests
{
    using System;
    using System.IO;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class ScriptTransformationTest : TransformationTestBase
    {
        [Test]
        public void Should_transform_many_templates_with_script()
        {
            const string Template1 = @"name: ${name}";
            const string Template2 = @"age: ${age}";

            const string Script1 = @"
template1.template => Name.txt
template2.template => ${age}.txt";

            this.CreateTemplateFile(Template1, "template1.template");
            this.CreateTemplateFile(Template2, "template2.template");
            this.CreateTemplateFile(Script1, "script1.script");

            new Script("script1.script")
                .Parameter("name", "João Carlos")
                .Parameter("age", "24")
                .Execute();

            File.Exists("Name.txt").ShouldBeTrue();
            File.Exists("24.txt").ShouldBeTrue();
            FileSystemUtil.GetAllText("Name.txt").ShouldEqual("name: João Carlos\r\n");
            FileSystemUtil.GetAllText("24.txt").ShouldEqual("age: 24\r\n");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_exception_if_script_file_is_null()
        {
            new Script(string.Empty);
        }
    }
}