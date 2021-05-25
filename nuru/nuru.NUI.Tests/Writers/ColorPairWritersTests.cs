using NUnit.Framework;
using nuru.NUI.Writers;
using System;

namespace nuru.NUI.Tests.Writers
{
    public class ColorPairWritersTests : ReadWriteBaseTests
    {
        IColorPairWriter voidWriter;
        IColorPairWriter uint4Writer;
        IColorPairWriter uint8Writer;

        public override void Setup()
        {
            base.Setup();
            voidWriter = new ColorPairVoidWriter();
            uint4Writer = new ColorPairUInt4Writer(stream);
            uint8Writer = new ColorPairUInt8Writer(stream);
        }

        [Test]
        public void TestWriteVoid()
        {
            voidWriter.Write(default);
        }

        [TestCase(0, 0, ExpectedResult = "00000000")]
        [TestCase(1, 2, ExpectedResult = "00010010")]
        [TestCase(0, 255, ExpectedResult = "00001111")]
        [TestCase(255, 0, ExpectedResult = "11110000")]
        public string TestWriteUInt4(int foreground, int background)
        {
            uint4Writer.Write(new ColorPair((byte)foreground, (byte)background));
            RewindStream();
            return ReadByteAsBinaryString();
        }

        // note: strings are big endian
        [TestCase(0, 0, ExpectedResult = "0000000000000000")] 
        [TestCase(1, 2, ExpectedResult = "0000001000000001")]
        [TestCase(0, 255, ExpectedResult = "1111111100000000")]
        [TestCase(255, 0, ExpectedResult = "0000000011111111")]
        public string TestWriteUInt8(int foreground, int background)
        {
            uint8Writer.Write(new ColorPair((byte)foreground, (byte)background));
            RewindStream();
            return ReadByteAsBinaryString() + ReadByteAsBinaryString();
        }

        private string ReadByteAsBinaryString()
        {
            return Convert.ToString(reader.ReadByte(), 2).PadLeft(8, '0');
        }
    }
}
