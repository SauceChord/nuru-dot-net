using NUnit.Framework;
using nuru.NUI.Writers;
using System;
using System.Collections.Generic;
using System.Text;

namespace nuru.NUI.Tests.Writers
{
    public class TestMetadataWriters : TestReadWriteBase
    {
        protected IMetadataWriter voidWriter;
        protected IMetadataWriter uint8Writer;
        protected IMetadataWriter uint16Writer;

        public override void Setup()
        {
            base.Setup();
            voidWriter = new MetadataVoidWriter();
            uint8Writer = new MetadataUInt8Writer(stream);
            uint16Writer = new MetadataUInt16Writer(stream);
        }

        [Test]
        public void TestWriteVoid()
        {
            voidWriter.Write(312);
            Assert.That(stream.Position, Is.EqualTo(0));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(254)]
        [TestCase(255)]
        public void TestWriteUInt8(int testCase)
        {
            uint8Writer.Write((ushort)testCase);
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
            uint16Writer.Write((ushort)testCase);
            Assert.That(stream.Position, Is.EqualTo(2));
            RewindStream();
            Assert.That(reader.ReadUInt16(), Is.EqualTo(testCase));
        }
    }
}
