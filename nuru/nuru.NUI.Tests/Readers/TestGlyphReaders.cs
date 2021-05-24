using NUnit.Framework;
using nuru.NUI.Readers;
using System;

namespace nuru.NUI.Tests.Readers
{
    public class TestGlyphReaders : TestReadWriteBase
    {
        protected IGlyphReader spaceReader;
        protected IGlyphReader asciiReader;
        protected IGlyphReader unicodeReader;

        public override void Setup()
        {
            base.Setup();
            spaceReader = new GlyphSpaceReader();
            asciiReader = new GlyphASCIIReader(stream);
            unicodeReader = new GlyphUnicodeReader(stream);
        }

        [Test]
        public void TestReadSpace()
        {
            Assert.That(spaceReader.Read(), Is.EqualTo(' '));
        }

        [TestCase('A')]
        [TestCase('B')]
        [TestCase('C')]
        [TestCase('Z')]
        public void TestReadASCII(char testCase)
        {
            writer.Write(Convert.ToByte(testCase));
            RewindStream();
            Assert.That(asciiReader.Read(), Is.EqualTo(testCase));
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
            Assert.That(unicodeReader.Read(), Is.EqualTo(testCase));
            Assert.That(stream.Position, Is.EqualTo(2));
        }
    }
}
