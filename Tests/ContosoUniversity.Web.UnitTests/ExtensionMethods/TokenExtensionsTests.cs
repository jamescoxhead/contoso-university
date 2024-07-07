using System.Globalization;
using System.Text;
using ContosoUniversity.Web.ExtensionMethods;

namespace ContosoUniversity.Web.UnitTests.ExtensionMethods;

public class TokenExtensionsTests
{
    [TestCase("Hello world")]
    [TestCase("Testing testing testing")]
    [TestCase("12345678")]
    public void GetLastCharsShouldGetLastBit(string input)
    {
        var byteInput = Encoding.UTF8.GetBytes(input);
        var expectedValue = byteInput[7].ToString(CultureInfo.InvariantCulture);

        var result = byteInput.GetLastChars();

        result.Should().Be(expectedValue);
    }
}
