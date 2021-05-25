using NUnit.Framework;
using nuru.NUI.Readers;
using System;

namespace nuru.NUI.Tests
{
    public class CellReaderFactoryTests : IGlyphReader, IColorPairReader, IMetadataReader
    {
        protected CellReaderFactory factory;
        protected CellMode bigCellMode;

        [SetUp]
        public void Setup()
        {
            factory = new CellReaderFactory();
            bigCellMode = new CellMode(
                            GlyphMode.UTF16,
                            ColorMode.EightBit,
                            MetadataMode.SixteenBit);
        }

        [Test]
        public void Build_GivenNoRegisteredReaders_ThrowsNoReaderException()
        {
            Assert.Throws<NoReaderException>(() => factory.Build(bigCellMode));
        }

        [TestCase(GlyphMode.None)]
        [TestCase(GlyphMode.ASCII)]
        [TestCase(GlyphMode.UTF16)]
        [TestCase(GlyphMode.Palette)]
        public void RegisterGlyphReader_GivenNull_ThrowsArgumentNullException(GlyphMode mode)
        {
            Assert.Throws<ArgumentNullException>(() => factory.RegisterGlyphReader(mode, null));
        }

        [TestCase(ColorMode.None)]
        [TestCase(ColorMode.FourBit)]
        [TestCase(ColorMode.EightBit)]
        [TestCase(ColorMode.Palette)]
        public void RegisterColorPairReader_GivenNull_ThrowsArgumentNullException(ColorMode mode)
        {
            Assert.Throws<ArgumentNullException>(() => factory.RegisterColorPairReader(mode, null));
        }

        [TestCase(MetadataMode.None)]
        [TestCase(MetadataMode.EightBit)]
        [TestCase(MetadataMode.SixteenBit)]
        public void RegisterMetadataReader_GivenNull_ThrowsArgumentNullException(MetadataMode mode)
        {
            Assert.Throws<ArgumentNullException>(() => factory.RegisterMetadataReader(mode, null));
        }

        [Test]
        public void Build_GivenReaders_ReturnsCellReader()
        {            
            factory.RegisterGlyphReader(bigCellMode.Glyph, this);
            factory.RegisterColorPairReader(bigCellMode.Color, this);
            factory.RegisterMetadataReader(bigCellMode.Metadata, this);

            var cellReader = factory.Build(bigCellMode);

            Assert.That(cellReader, Is.Not.EqualTo(null));
            Assert.That(cellReader.Read().Character, Is.EqualTo(' '));
            Assert.That(cellReader.Read().Colors.Foreground, Is.EqualTo(1));
            Assert.That(cellReader.Read().Colors.Background, Is.EqualTo(2));
            Assert.That(cellReader.Read().Metadata, Is.EqualTo(200));
        }

        char IGlyphReader.Read()
        {
            return ' ';
        }

        ColorPair IColorPairReader.Read()
        {
            return new ColorPair(1, 2);
        }

        ushort IMetadataReader.Read()
        {
            return 200;
        }
    }
}
