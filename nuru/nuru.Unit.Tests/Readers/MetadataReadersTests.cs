using NUnit.Framework;
using nuru.NUI.Readers;

namespace nuru.NUI.Tests.Readers
{
    public class MetadataReadersTests : ReadWriteBaseTests
    {
        protected IMetadataReader voidReader;
        protected IMetadataReader uint8Reader;
        protected IMetadataReader uint16Reader;

        public override void Setup()
        {
            base.Setup();
            voidReader = new MetadataVoidReader();
            uint8Reader = new MetadataUInt8Reader();
            uint16Reader = new MetadataUInt16Reader();
        }

        [Test]
        public void TestReadVoid()
        {
            Assert.That(voidReader.Read(reader), Is.EqualTo(0));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(254)]
        [TestCase(255)]
        public void TestReadUInt8(int testCase)
        {
            writer.Write((byte)testCase);
            RewindStream();
            Assert.That(uint8Reader.Read(reader), Is.EqualTo(testCase));
            Assert.That(stream.Position, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(65534)]
        [TestCase(65535)]
        public void TestReadUInt16(int testCase)
        {
            writer.WriteBigEndian((ushort)testCase);
            RewindStream();
            Assert.That(uint16Reader.Read(reader), Is.EqualTo(testCase));
            Assert.That(stream.Position, Is.EqualTo(2));
        }
    }
}
