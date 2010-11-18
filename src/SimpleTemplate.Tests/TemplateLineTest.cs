namespace SimpleTemplate.Tests
{
    using System;
    using System.Collections.Generic;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class TemplateLineTest
    {
        private Dictionary<string, string> parameters;

        [SetUp]
        public void Setup()
        {
            this.parameters = new Dictionary<string, string>
            {
                { "name", "João" },
                { "age", "24" }
            };
        }

        [Test]
        public void Should_parse_template_line_with_one_variable()
        {
            var line = new TemplateLine("My name is ${name}", this.parameters).Parse();
            line.Variables.Contains("name").ShouldBeTrue();
            line.ForEach.ShouldBeNull();
            line.Content.ShouldEqual("My name is ${name}");
        }

        [Test]
        public void Should_parse_template_line_with_two_variable()
        {
            var line = new TemplateLine("My name is ${name} and im ${age} years old", this.parameters).Parse();
            line.Variables.Contains("name").ShouldBeTrue();
            line.Variables.Contains("age").ShouldBeTrue();
            line.ForEach.ShouldBeNull();
            line.Content.ShouldEqual("My name is ${name} and im ${age} years old");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_throw_exception_when_parse_line_with_invalid_syntax()
        {
            new TemplateLine("My name is ${name", this.parameters).Parse();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Should_throw_exception_when_parse_line_with_one_variable_and_invalid_syntax()
        {
            new TemplateLine("My name is ${name} and ${age years old", this.parameters).Parse();
        }

        ////[Test]
        ////[ExpectedException(typeof(InvalidOperationException))]
        ////public void Should_throw_exception_when_parse_variable_not_declared()
        ////{
        ////    var emptyParameters = new Dictionary<string, string>();
        ////    new TemplateLine("My name is ${name}", emptyParameters).Parse();
        ////}

        [Test]
        public void Should_not_parse_if_not_exists_variables()
        {
            var line = new TemplateLine("Dont exists variables here", this.parameters).Parse();
            line.Variables.ShouldBeEmpty();
            line.ForEach.ShouldBeNull();
            line.Content.ShouldEqual("Dont exists variables here");
        }

        [Test]
        public void Should_parse_foreach_start_with_array_variable()
        {
            var line = new TemplateLine("${foreach childrens}", this.parameters).Parse();
            line.Variables.ShouldBeEmpty();
            line.ForEach.Variable.ShouldEqual("childrens");
            line.ForEach.IsStart.ShouldBeTrue();
            line.ForEach.IsEnd.ShouldBeFalse();
            line.Content.ShouldEqual("${foreach childrens}");
        }

        [Test]
        public void Should_parse_foreach_end_with_array_variable()
        {
            var line = new TemplateLine("${end foreach}", this.parameters).Parse();
            line.Variables.ShouldBeEmpty();
            line.ForEach.Variable.ShouldBeNull();
            line.ForEach.IsStart.ShouldBeFalse();
            line.ForEach.IsEnd.ShouldBeTrue();
            line.Content.ShouldEqual("${end foreach}");
        }
    }
}