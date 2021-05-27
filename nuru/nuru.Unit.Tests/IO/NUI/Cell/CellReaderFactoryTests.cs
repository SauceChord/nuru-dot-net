using BigEndian.IO;
using NUnit.Framework;
using System;

namespace nuru.IO.NUI.Unit.Tests
{
    public class CellReaderFactoryTests : IGlyphReader, IColorReader, IMetadataReader
    {
        protected CellReaderFactory factory;
        protected NUICellConfig bigCellConfig;

        [SetUp]
        public void Setup()
        {
            factory = new CellReaderFactory();
            bigCellConfig = new NUICellConfig(
                            GlyphMode.UTF16,
                            ColorMode.EightBit,
                            MetadataMode.SixteenBit);
        }

        [Test]
        public void Build_GivenNoRegisteredReaders_ThrowsNoReaderException()
        {
            Assert.Throws<CellFactoryException>(() => factory.Build(bigCellConfig));
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
        public void RegisterColorReader_GivenNull_ThrowsArgumentNullException(ColorMode mode)
        {
            Assert.Throws<ArgumentNullException>(() => factory.RegisterColorReader(mode, null));
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
            factory.RegisterGlyphReader(bigCellConfig.Glyph, this);
            factory.RegisterColorReader(bigCellConfig.Color, this);
            factory.RegisterMetadataReader(bigCellConfig.Metadata, this);

            var cellReader = factory.Build(bigCellConfig);

            Assert.That(cellReader, Is.Not.EqualTo(null));
            Assert.That(cellReader.Read(null).Glyph, Is.EqualTo(' '));
            Assert.That(cellReader.Read(null).Color.Foreground, Is.EqualTo(1));
            Assert.That(cellReader.Read(null).Color.Background, Is.EqualTo(2));
            Assert.That(cellReader.Read(null).Metadata, Is.EqualTo(200));
        }

        char IGlyphReader.Read(BigEndianBinaryReader reader)
        {
            return ' ';
        }

        NUIColor IColorReader.Read(BigEndianBinaryReader reader)
        {
            return new NUIColor(1, 2);
        }

        ushort IMetadataReader.Read(BigEndianBinaryReader reader)
        {
            return 200;
        }
    }
}
