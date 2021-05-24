﻿using NUnit.Framework;
using nuru.NUI.Readers;

namespace nuru.NUI.Tests.Readers
{
    public class TestMetadataReaders : TestReadWriteBase
    {
        protected IMetadataReader voidReader;
        protected IMetadataReader uint8Reader;
        protected IMetadataReader uint16Reader;

        public override void Setup()
        {
            base.Setup();
            voidReader = new MetadataVoidReader();
            uint8Reader = new MetadataUInt8Reader(stream);
            uint16Reader = new MetadataUInt16Reader(stream);
        }

        [Test]
        public void TestReadVoid()
        {
            Assert.That(voidReader.Read(), Is.EqualTo(0));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(254)]
        [TestCase(255)]
        public void TestReadUInt8(int testCase)
        {
            writer.Write((byte)testCase);
            RewindStream();
            Assert.That(uint8Reader.Read(), Is.EqualTo(testCase));
            Assert.That(stream.Position, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(65534)]
        [TestCase(65535)]
        public void TestReadUInt16(int testCase)
        {
            writer.Write((ushort)testCase);
            RewindStream();
            Assert.That(uint16Reader.Read(), Is.EqualTo(testCase));
            Assert.That(stream.Position, Is.EqualTo(2));
        }
    }
}
