
namespace SimpleTemplate.Tests
{
    using System;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class ParameterTest
    {
        [Test]
        public void Should_parse_parameter()
        {
            var parameter = new Parameter("name:João").Parse();

            parameter.Key.ShouldEqual("name");
            parameter.Value.ShouldEqual("João");
        }

        [Test]
        public void Should_parse_array_parameter()
        {
            var parameter = new Parameter(
                "childrens:{name:João,age:24},{name:Emilia,age:30},{name:Carolina,age:34}").Parse();

            parameter.Key.ShouldEqual("childrens");
            parameter.Value.ShouldEqual("{name:João,age:24},{name:Emilia,age:30},{name:Carolina,age:34}");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_throw_exception_when_parameter_dont_have_value()
        {
            new Parameter("childrens:").Parse();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_throw_exception_when_parameter_dont_have_name()
        {
            new Parameter(":João").Parse();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_throw_exception_when_parameter_has_invalid_syntax()
        {
            new Parameter("name=João").Parse();
        }
    }
}
