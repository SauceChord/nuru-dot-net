using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryColorPairReader
    {
        MemoryStream stream;
        BinaryWriter writer;
        BinaryColorPairReader colorReader;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
            colorReader = new BinaryColorPairReader(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
        }

        [TestCase(0x01, ExpectedResult = "0, 1")]
        [TestCase(0x11, ExpectedResult = "1, 1")]
        [TestCase(0xf2, ExpectedResult = "15, 2")]
        [TestCase(0xff, ExpectedResult = "15, 15")]
        public string Test4BitsPerChannel(int value)
        {
            writer.Write((byte)value);
            stream.Position = 0;

            return colorReader.Read4BitsPerChannel().ToString();
        }

        [TestCase(0, 1, ExpectedResult = "0, 1")]
        [TestCase(1, 1, ExpectedResult = "1, 1")]
        [TestCase(15, 2, ExpectedResult = "15, 2")]
        [TestCase(15, 15, ExpectedResult = "15, 15")]
        [TestCase(255, 15, ExpectedResult = "255, 15")]
        [TestCase(0, 255, ExpectedResult = "0, 255")]
        public string Test8BitsPerChannel(int fg, int bg)
        {
            writer.Write((byte)fg);
            writer.Write((byte)bg);
            stream.Position = 0;

            return colorReader.Read8BitsPerChannel().ToString();
        }
    }
}
