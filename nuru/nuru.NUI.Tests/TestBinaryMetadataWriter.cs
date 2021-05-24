using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryMetadataWriter
    {
        MemoryStream stream;
        BinaryReader reader;
        BinaryMetadataWriter metadataWriter;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);
            metadataWriter = new BinaryMetadataWriter(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(255, ExpectedResult = 255)]
        public byte Test8BitMetadata(int value)
        {
            metadataWriter.Write8Bits((byte)value);

            stream.Position = 0;
            return reader.ReadByte();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(255, ExpectedResult = 255)]
        [TestCase(65535, ExpectedResult = 65535)]
        public ushort Test16BitMetadata(int value)
        {
            metadataWriter.Write16Bits((ushort)value);
            
            stream.Position = 0;
            return reader.ReadUInt16();
        }
    }
}
