namespace SimpleTemplate.Tests
{
    using System;
    using System.Collections.Generic;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class ScriptLineTest
    {
        [Test]
        public void Should_parse_script_line_template_reference()
        {
            var scriptLine = new ScriptLine("controller.template => Controller.cs").Parse();

            scriptLine.TemplateFile.ShouldEqual("controller.template");
            scriptLine.TransformationFile.ShouldEqual("Controller.cs");
        }

        [Test]
        public void Should_parse_script_line_template_with_path()
        {
            var scriptLine = new ScriptLine("scaffold\\controller.template => Controller.cs").Parse();

            scriptLine.TemplateFile.ShouldEqual("scaffold\\controller.template");
            scriptLine.TransformationFile.ShouldEqual("Controller.cs");
        }

        [Test]
        public void Should_parse_script_line_using_parameters_on_transformation_file()
        {
            var parameters = new Dictionary<string, string> { { "entity", "UserGroup" } };

            var scriptLine = new ScriptLine(
                "template1.template => ${entity}Controller.cs", parameters)
                .Parse();

            scriptLine.TemplateFile.ShouldEqual("template1.template");
            scriptLine.TransformationFile.ShouldEqual("UserGroupController.cs");
        }

        [Test]
        public void Should_parse_script_line_using_parameters_on_path_of_transformation_file()
        {
            var parameters = new Dictionary<string, string> { { "entity", "UserGroup" } };

            var scriptLine = new ScriptLine(
                "view_edit.template => Web/Views/${entity}/Edit.aspx", parameters)
                .Parse();

            scriptLine.TemplateFile.ShouldEqual("view_edit.template");
            scriptLine.TransformationFile.ShouldEqual("Web/Views/UserGroup/Edit.aspx");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dont_should_parse_script_line_with_invalid_token()
        {
            new ScriptLine("controller.template: Controller.cs").Parse();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dont_should_parse_script_line_with_invalid_template_file()
        {
            new ScriptLine("=> Controller.cs").Parse();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dont_should_parse_script_line_with_invalid_transformation_file()
        {
            new ScriptLine("controller.template =>").Parse();
        }
    }
}