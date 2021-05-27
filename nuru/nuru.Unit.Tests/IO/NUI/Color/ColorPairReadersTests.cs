using NUnit.Framework;
using nuru.IO.NUI.Cell.Color;

namespace nuru.Unit.Tests.IO.NUI
{
    public class ColorReadersTests : ReadWriteBaseTests
    {
        protected IColorReader voidReader;
        protected IColorReader uint4Reader;
        protected IColorReader uint8Reader;

        public override void Setup()
        {
            base.Setup();
            voidReader = new ColorVoidReader();
            uint4Reader = new ColorUInt4Reader();
            uint8Reader = new ColorUInt8Reader();
        }

        [Test]
        public void TestReadVoid()
        {
            Assert.That(voidReader.Read(null), Is.EqualTo(default(ColorData)));
        }

        [TestCase(0x00, ExpectedResult = "0, 0")]
        [TestCase(0xa0, ExpectedResult = "10, 0")]
        [TestCase(0x0a, ExpectedResult = "0, 10")]
        [TestCase(0xff, ExpectedResult = "15, 15")]
        public string TestReadUInt4(int nibbles)
        {
            writer.Write((byte)nibbles);
            RewindStream();
            return uint4Reader.Read(reader).ToString();
        }

        [Ignore("Waiting for domsson's say about endianness")]
        [TestCase(0x0000, ExpectedResult = "0, 0")]
        [TestCase(0x00fe, ExpectedResult = "0, 254")]
        [TestCase(0xfe00, ExpectedResult = "254, 0")]
        [TestCase(0xffff, ExpectedResult = "255, 255")]
        public string TestReadUInt8(int nibbles)
        {
            writer.Write((ushort)nibbles);
            RewindStream();
            return uint8Reader.Read(reader).ToString();
        }
    }
}
