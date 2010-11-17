namespace SimpleTemplate.Tests
{
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class CommandLineToArgsTest
    {
        [Test]
        public void Should_parse_arguments()
        {
            var args = CommandLine.ToArgs("\"this is a text\" \"other text\"");

            args[0].ShouldEqual("this is a text");
            args[1].ShouldEqual("other text");
        }
    }
}
