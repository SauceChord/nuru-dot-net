using System.IO;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryGlyphWriter
    {
        MemoryStream stream;
        BinaryReader reader;
        BinaryGlyphWriter glyphWriter;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();            
            reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);            
            glyphWriter = new BinaryGlyphWriter(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
            reader.Dispose();
        }

        [Test]
        public void TestASCII()
        {
            glyphWriter.WriteASCII('A');
            glyphWriter.WriteASCII('B');
            glyphWriter.WriteASCII('C');

            stream.Position = 0;

            Assert.That(stream.Length, Is.EqualTo(3));
            Assert.That(reader.ReadByte(), Is.EqualTo(65));
            Assert.That(reader.ReadByte(), Is.EqualTo(66));
            Assert.That(reader.ReadByte(), Is.EqualTo(67));
        }

        [Test]
        public void TestUTF16()
        {
            glyphWriter.WriteUTF16('A');
            glyphWriter.WriteUTF16('B');
            glyphWriter.WriteUTF16('C');

            stream.Position = 0;

            Assert.That(stream.Length, Is.EqualTo(6));
            Assert.That(reader.ReadChar(), Is.EqualTo('A'));
            Assert.That(reader.ReadChar(), Is.EqualTo('B'));
            Assert.That(reader.ReadChar(), Is.EqualTo('C'));
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
