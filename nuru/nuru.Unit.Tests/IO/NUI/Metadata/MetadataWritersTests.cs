using NUnit.Framework;
using nuru.IO.NUI.Cell.Metadata;
using nuru.Unit.Tests;

namespace nuru.IO.NUI.Unit.Tests
{
    public class MetadataWritersTests : ReadWriteBaseTests
    {
        protected IMetadataWriter voidWriter;
        protected IMetadataWriter uint8Writer;
        protected IMetadataWriter uint16Writer;

        public override void Setup()
        {
            base.Setup();
            voidWriter = new MetadataVoidWriter();
            uint8Writer = new MetadataUInt8Writer();
            uint16Writer = new MetadataUInt16Writer();
        }

        [Test]
        public void TestWriteVoid()
        {
            voidWriter.Write(writer, 312);
            Assert.That(stream.Position, Is.EqualTo(0));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(254)]
        [TestCase(255)]
        public void TestWriteUInt8(int testCase)
        {
            uint8Writer.Write(writer, (ushort)testCase);
            Assert.That(stream.Position, Is.EqualTo(1));
            RewindStream();
            Assert.That(reader.ReadByte(), Is.EqualTo(testCase));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(65534)]
        [TestCase(65535)]
        public void TestWriteUInt16(int testCase)
        {
            uint16Writer.Write(writer, (ushort)testCase);
            Assert.That(stream.Position, Is.EqualTo(2));
            RewindStream();
            Assert.That(reader.ReadBigEndianUInt16(), Is.EqualTo(testCase));
        }
    }
}
