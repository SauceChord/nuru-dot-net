using NUnit.Framework;

namespace nuru.Unit.Tests
{
    public class RGBTests
    {
        [TestCase(0x0020ff00, ExpectedResult = "20FF00")]
        [TestCase(0x7f010203, ExpectedResult = "010203")]
        public string ToString_ReturnsHexTriple(int rgbInt)
        {
            var rgb = new RGB(rgbInt);
            return rgb.ToString();
        }
    }
}
