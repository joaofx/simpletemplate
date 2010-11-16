namespace SimpleTemplate.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class SimpleTransformationTest : TransformationTestBase
    {
        private string Template()
        {
            return
                @"
name: ${name}
age: ${age}";
        }

        [Test]
        public void Should_generate_simple_template()
        {
            this.CreateTemplateFile(this.Template(), this.templateFile);

            this.template
                .Parameter("name", "João")
                .Parameter("age", "24")
                .Transform("simple.txt");

            this.AssertFile("simple.txt", "\r\nname: João\r\nage: 24\r\n");
        }
    }
}