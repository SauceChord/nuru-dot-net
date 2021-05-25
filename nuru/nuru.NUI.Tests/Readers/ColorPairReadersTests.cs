using NUnit.Framework;
using nuru.NUI.Readers;

namespace nuru.NUI.Tests.Readers
{
    public class TestColorPairReaders : ReadWriteBaseTests
    {
        protected IColorPairReader voidReader;
        protected IColorPairReader uint4Reader;
        protected IColorPairReader uint8Reader;

        public override void Setup()
        {
            base.Setup();
            voidReader = new ColorPairVoidReader();
            uint4Reader = new ColorPairUInt4Reader(stream);
            uint8Reader = new ColorPairUInt8Reader(stream);
        }

        [Test]
        public void TestReadVoid()
        {
            Assert.That(voidReader.Read(), Is.EqualTo(default(ColorPair)));
        }

        [TestCase(0x00, ExpectedResult = "0, 0")]
        [TestCase(0xa0, ExpectedResult = "10, 0")]
        [TestCase(0x0a, ExpectedResult = "0, 10")]
        [TestCase(0xff, ExpectedResult = "15, 15")]
        public string TestReadUInt4(int nibbles)
        {
            writer.Write((byte)nibbles);
            RewindStream();
            return uint4Reader.Read().ToString();
        }

        [TestCase(0x0000, ExpectedResult = "0, 0")]
        [TestCase(0x00fe, ExpectedResult = "0, 254")]
        [TestCase(0xfe00, ExpectedResult = "254, 0")]
        [TestCase(0xffff, ExpectedResult = "255, 255")]
        public string TestReadUInt8(int nibbles)
        {
            writer.Write((ushort)nibbles);
            RewindStream();
            return uint8Reader.Read().ToString();
        }
    }
}
