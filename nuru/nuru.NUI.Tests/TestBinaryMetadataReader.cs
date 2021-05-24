using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryMetadataReader
    {
        MemoryStream stream;
        BinaryWriter writer;
        BinaryMetadataReader metadataReader;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
            metadataReader = new BinaryMetadataReader(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(255, ExpectedResult = 255)]
        public ushort Test8BitMetadata(int value)
        {
            writer.Write((byte)value);
            stream.Position = 0;

            return metadataReader.Read8Bits();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(255, ExpectedResult = 255)]
        [TestCase(65530, ExpectedResult = 65530)]
        public ushort Test16BitMetadata(int value)
        {
            writer.Write((ushort)value);
            stream.Position = 0;

            return metadataReader.Read16Bits();
        }
    }
}
