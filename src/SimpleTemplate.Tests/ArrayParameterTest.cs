
namespace SimpleTemplate.Tests
{
    using System.Linq;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class ArrayParameterTest
    {
        [Test]
        public void Should_parse_array_parameter()
        {
            const string Text = "{name:João,age:24},{name:Emilia,age:30},{name:Carolina,age:34}";

            var arrayParameter = new ArrayParameter(Text).Parse();

            arrayParameter.Count.ShouldEqual(3);

            arrayParameter[0].ElementAt(0).Key.ShouldEqual("name");
            arrayParameter[0].ElementAt(1).Key.ShouldEqual("age");
            arrayParameter[1].ElementAt(0).Key.ShouldEqual("name");
            arrayParameter[1].ElementAt(1).Key.ShouldEqual("age");
            arrayParameter[2].ElementAt(0).Key.ShouldEqual("name");
            arrayParameter[2].ElementAt(1).Key.ShouldEqual("age");

            arrayParameter[0].ElementAt(0).Value.ShouldEqual("João");
            arrayParameter[0].ElementAt(1).Value.ShouldEqual("24");
            arrayParameter[1].ElementAt(0).Value.ShouldEqual("Emilia");
            arrayParameter[1].ElementAt(1).Value.ShouldEqual("30");
            arrayParameter[2].ElementAt(0).Value.ShouldEqual("Carolina");
            arrayParameter[2].ElementAt(1).Value.ShouldEqual("34");
        }
    }
}
