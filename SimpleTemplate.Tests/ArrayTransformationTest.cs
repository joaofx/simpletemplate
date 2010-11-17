namespace SimpleTemplate.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ArrayTransformationTest : TransformationTestBase
    {
        private string Template()
        {
            return @"
name: ${name}
${foreach childrens}
    children: ${name}
    age: ${age}
${end foreach}";
        }

        [Test]
        public void Should_transform_template_with_array()
        {
            this.CreateTemplateFile(this.Template(), this.templateFile);

            this.template
                .Parameter("name", "João")
                .Parameter("age", "52")
                .Parameter("childrens", "{name:João,age:24},{name:Emilia,age:30},{name:Carolina,age:34}")
                .Transform("simple.txt");

            const string Expected = @"
name: João
    children: João
    age: 24
    children: Emilia
    age: 30
    children: Carolina
    age: 34
";

            this.AssertFile("simple.txt", Expected);
        }
    }
}