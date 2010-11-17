namespace SimpleTemplate.Tests
{
    using NUnit.Framework;
    using NBehave.Spec.NUnit;

    [TestFixture]
    public class ProjectTest
    {
        [Test]
        public void Should_include_file_on_csproj()
        {
            var project = new Project("csproj.xml");
            project.IncludeCompileFile("NewFile.txt");

            project.FindCompileIncludeFile("NewFile.txt").Count.ShouldEqual(1);
        }

        [Test]
        public void Should_ignore_relative_path()
        {
            var project = new Project("csproj.xml");
            project.IncludeCompileFile("..\\..\\Relative.txt");
            project.IncludeCompileFile("../../OtherRelative.txt");

            project.FindCompileIncludeFile("Relative.txt").Count.ShouldEqual(1);
            project.FindCompileIncludeFile("OtherRelative.txt").Count.ShouldEqual(1);
        }

        [Test]
        public void Should_not_include_one_file_many_times()
        {
            var project = new Project("csproj.xml");
            project.IncludeCompileFile("NotRepeat.txt");
            project.IncludeCompileFile("NotRepeat.txt");

            project.FindCompileIncludeFile("NotRepeat.txt").Count.ShouldEqual(1);
        }
    }
}
