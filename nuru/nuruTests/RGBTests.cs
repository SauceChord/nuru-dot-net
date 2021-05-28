using NUnit.Framework;

namespace nuru
{
    public class RGBTests
    {
        [Test]
        public void Constructor_Initializes_Fields()
        {
            RGB rgb = new RGB(1, 2, 3);

            Assert.That(rgb.Red, Is.EqualTo(1));
            Assert.That(rgb.Green, Is.EqualTo(2));
            Assert.That(rgb.Blue, Is.EqualTo(3));
        }

        [Test]
        public void ToString_Formats_HexTriplet()
        {
            RGB rgb = new RGB(0xf1, 0x02, 0xaa);

            Assert.That(rgb.ToString(), Is.EqualTo("F102AA"));
        }
    }
}