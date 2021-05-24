using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryColorPairWriter
    {
        MemoryStream stream;
        BinaryReader reader;
        BinaryColorPairWriter colorWriter;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);
            colorWriter = new BinaryColorPairWriter(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
        }

        [TestCase(0, 0, ExpectedResult = "00000000")]
        [TestCase(1, 2, ExpectedResult = "00010010")]
        [TestCase(0, 255, ExpectedResult = "00001111")]
        [TestCase(255, 0, ExpectedResult = "11110000")]
        public string Test4BitsPerChannel(int fg, int bg)
        {
            colorWriter.Write4BitsPerChannel(new ColorPair((byte)fg, (byte)bg));
            stream.Position = 0;
            return ReadByteAsBinaryString();
        }

        [TestCase(0, 0, ExpectedResult = "0000000000000000")]
        [TestCase(1, 0, ExpectedResult = "0000000100000000")]
        [TestCase(0, 1, ExpectedResult = "0000000000000001")]
        [TestCase(255, 0, ExpectedResult = "1111111100000000")]
        [TestCase(0, 255, ExpectedResult = "0000000011111111")]
        public string Test8BitsPerChannel(int fg, int bg)
        {
            colorWriter.Write8BitsPerChannel(new ColorPair((byte)fg, (byte)bg));
            stream.Position = 0;
            return ReadByteAsBinaryString() + ReadByteAsBinaryString();
        }

        private string ReadByteAsBinaryString()
        {
            return Convert.ToString(reader.ReadByte(), 2).PadLeft(8, '0');
        }
    }
}
