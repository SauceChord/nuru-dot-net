using NUnit.Framework;

namespace nuru.Unit.Tests
{
    public class ColorTests
    {
        [TestCase(0, 1, ExpectedResult = "0, 1")]
        [TestCase(255, 0, ExpectedResult = "255, 0")]
        [TestCase(125, 200, ExpectedResult = "125, 200")]
        public string TestToString(byte fg, byte bg)
        {
            return new Color(fg, bg).ToString();
        }
    }
}
