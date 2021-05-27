using NUnit.Framework;
using nuru.IO.NUI.Cell.Glyph;
using nuru.Unit.Tests;
using System;

namespace nuru.IO.NUI.Unit.Tests
{
    public class GlyphReadersTests : ReadWriteBaseTests
    {
        protected IGlyphReader spaceReader;
        protected IGlyphReader asciiReader;
        protected IGlyphReader unicodeReader;

        public override void Setup()
        {
            base.Setup();
            spaceReader = new GlyphSpaceReader();
            asciiReader = new GlyphASCIIReader();
            unicodeReader = new GlyphUnicodeReader();
        }

        [Test]
        public void TestReadSpace()
        {
            Assert.That(spaceReader.Read(reader), Is.EqualTo(' '));
        }

        [TestCase('A')]
        [TestCase('B')]
        [TestCase('C')]
        [TestCase('Z')]
        public void TestReadASCII(char testCase)
        {
            writer.Write(Convert.ToByte(testCase));
            RewindStream();
            Assert.That(asciiReader.Read(reader), Is.EqualTo(testCase));
            Assert.That(stream.Position, Is.EqualTo(1));
        }

        [TestCase('A')]
        [TestCase('B')]
        [TestCase('C')]
        [TestCase('Å')]
        public void TestReadUnicode(char testCase)
        {
            writer.Write(testCase);
            RewindStream();
            Assert.That(unicodeReader.Read(reader), Is.EqualTo(testCase));
            Assert.That(stream.Position, Is.EqualTo(2));
        }
    }
}
