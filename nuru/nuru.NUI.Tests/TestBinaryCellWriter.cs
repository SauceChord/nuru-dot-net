using System;
using System.IO;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryCellWriter
    {
        MemoryStream stream;
        BinaryGlyphReader glyphReader;
        BinaryColorPairReader colorReader;
        BinaryMetadataReader metadataReader;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            glyphReader = new BinaryGlyphReader(stream);
            colorReader = new BinaryColorPairReader(stream);
            metadataReader = new BinaryMetadataReader(stream);
        }

        [Test]
        public void TestConstructor()
        {
            var mode = new CellMode(GlyphMode.UTF16, ColorMode.FourBit, MetadataMode.EightBit);

            var writer = new BinaryCellWriter(stream, mode);

            Assert.That(writer.BaseStream, Is.EqualTo(stream));
            Assert.That(writer.Mode, Is.EqualTo(mode));
        }

        [Test]
        public void TestWriteCell()
        {
            var mode = new CellMode(GlyphMode.ASCII, ColorMode.FourBit, MetadataMode.EightBit);
            var writer = new BinaryCellWriter(stream, mode);
            var cell = new Cell('A', 12, 2, 99);

            writer.WriteCell(cell);

            stream.Position = 0;
            Assert.That(stream.Length, Is.EqualTo(3));
            Assert.That(glyphReader.ReadASCII(), Is.EqualTo(cell.Character));
            Assert.That(colorReader.Read4BitsPerChannel(), Is.EqualTo(cell.Colors));
            Assert.That(metadataReader.Read8Bits(), Is.EqualTo(cell.Metadata));
        }
    }
}
