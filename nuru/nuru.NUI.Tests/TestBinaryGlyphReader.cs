using System.IO;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryGlyphReader
    {
        MemoryStream stream;
        BinaryWriter writer;
        BinaryGlyphReader glyphReader;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();            
            writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
            glyphReader = new BinaryGlyphReader(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
            writer.Dispose();
        }

        [TestCase(65, ExpectedResult = 'A')]
        [TestCase(66, ExpectedResult = 'B')]
        [TestCase(67, ExpectedResult = 'C')]
        public char TestASCII(int code)
        {
            writer.Write((byte)code);
            stream.Position = 0;

            return glyphReader.ReadASCII();
        }

        [TestCase('A', ExpectedResult = 'A')]
        [TestCase('B', ExpectedResult = 'B')]
        [TestCase('C', ExpectedResult = 'C')]
        public char TestUTF16(char value)
        {
            writer.Write(value);
            stream.Position = 0;

            return glyphReader.ReadUTF16();
        }

        [Ignore("Need further information")]
        [Test]
        public void TestPalette()
        {
            // To be honest, I am not entirely sure what needs to go on here.
            // Might have to look it up with domsson if I can't figure things
            // out from their source code.
        }
    }
}
