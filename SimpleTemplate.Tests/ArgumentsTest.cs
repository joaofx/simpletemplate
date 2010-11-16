namespace SimpleTemplate.Tests
{
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class ArgumentsTest
    {
        [Test]
        public void Should_parse_arguments()
        {
            const string CommandLineArgs = "all.script \"entity:UserGroup\" \"entitycamel:userGroup\" " +
                                       "\"title:Grupo de usuário\" \"testcategory:Segurança\" " +
                                       "\"attr:{name:Name,type:string},{name:Active,type:bool}\"";

            var arguments = new Arguments(CommandLine.ToArgs(CommandLineArgs)).Parse();

            arguments.Script.ShouldEqual("all.script");
            arguments.Parameters["entity"].ShouldEqual("UserGroup");
            arguments.Parameters["entitycamel"].ShouldEqual("userGroup");
            arguments.Parameters["title"].ShouldEqual("Grupo de usuário");
            arguments.Parameters["testcategory"].ShouldEqual("Segurança");
            arguments.Parameters["attr"].ShouldEqual("{name:Name,type:string},{name:Active,type:bool}");
        }
    }
}
