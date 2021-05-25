using System;
using System.IO;
using System.Text;
using BigEndian.IO;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class ImageLoaderTests
    {
        const byte VersionOne = 1;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLoadWithNullReader()
        {
            var exception = Assert.Throws<System.ArgumentNullException>(() => ImageLoader.LoadHeader(null));
            Assert.That(exception.ParamName, Is.EqualTo("reader"));
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'reader')"));
        }

        [Test]
        public void TestLoadWithEmptyReader()
        {
            var emptyStream = new MemoryStream();
            var emptyReader = new BigEndianBinaryReader(emptyStream);

            var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(emptyReader));
            Assert.That(exception.Message, Is.EqualTo("No valid NUI signature found."));
        }

        [Test]
        public void TestLoadWithBadSignature()
        {
            // Proper signature should be NURUIMG
            // Here we supply it with a zero length string.
            var badSigStream = new MemoryStream(new byte[7]);
            var badSigReader = new BigEndianBinaryReader(badSigStream);

            var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(badSigReader));
            Assert.That(exception.Message, Is.EqualTo("No valid NUI signature found."));
        }

        [TestCase(0, 0)]
        [TestCase(2, 255)]
        public void TestLoadWithUnsupportedVersion(byte badRangeStart, byte badRangeEnd)
        {
            for (byte badVersion = badRangeStart; badVersion < badRangeEnd; ++badVersion)
            {
                var badVersionStream = new MemoryStream();
                var badVersionWriter = new BigEndianBinaryWriter(badVersionStream);
                badVersionWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badVersionWriter.Write(badVersion);

                badVersionStream.Position = 0;
                var badVersionReader = new BigEndianBinaryReader(badVersionStream);

                var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(badVersionReader));
                Assert.That(exception.Message, Is.EqualTo($"Unsupported version '{badVersion}'."));
            }
        }

        [TestCase(3, 128)]
        [TestCase(130, 255)]
        public void TestLoadWithBadGlyphMode(byte badRangeStart, byte badRangeEnd)
        {
            for (byte badGlyph = badRangeStart; badGlyph < badRangeEnd; ++badGlyph)
            {
                var badGlyphStream = new MemoryStream();
                var badGlyphWriter = new BigEndianBinaryWriter(badGlyphStream);
                badGlyphWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badGlyphWriter.Write(VersionOne);
                badGlyphWriter.Write(badGlyph);
                badGlyphStream.Position = 0;
                var badGlyphReader = new BigEndianBinaryReader(badGlyphStream);

                var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(badGlyphReader));
                Assert.That(exception.Message, Is.EqualTo($"Unknown glyph mode '{badGlyph}'."));
            }
        }

        [TestCase(3, 129)]
        [TestCase(131, 255)]
        public void TestLoadWithBadColorMode(byte badRangeStart, byte badRangeEnd)
        {
            for (byte badColor = badRangeStart; badColor < badRangeEnd; ++badColor)
            {
                var badColorStream = new MemoryStream();
                var badColorWriter = new BigEndianBinaryWriter(badColorStream);
                badColorWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badColorWriter.Write(VersionOne);
                badColorWriter.Write((byte)GlyphMode.None);
                badColorWriter.Write(badColor);
                badColorStream.Position = 0;
                var badColorReader = new BigEndianBinaryReader(badColorStream);

                var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(badColorReader));
                Assert.That(exception.Message, Is.EqualTo($"Unknown color mode '{badColor}'."));
            }
        }

        [Test]
        public void TestLoadWithBadGlyphAndColorMode()
        {
            var badColorStream = new MemoryStream();
            var badColorWriter = new BigEndianBinaryWriter(badColorStream);
            badColorWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            badColorWriter.Write(VersionOne);
            badColorWriter.Write((byte)GlyphMode.None);
            badColorWriter.Write((byte)ColorMode.None); // Docs say cant be none if glyph is none
            badColorStream.Position = 0;
            var badColorReader = new BigEndianBinaryReader(badColorStream);

            var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(badColorReader));
            Assert.That(exception.Message, Is.EqualTo("Color mode can't be None (0) when glyph mode is set to None (0)."));
        }

        [TestCase(3, 255)]
        public void TestLoadWithBadMetadataMode(byte badRangeStart, byte badRangeEnd)
        {
            for (byte badMetadata = badRangeStart; badMetadata < badRangeEnd; ++badMetadata)
            {
                var badMetadataStream = new MemoryStream();
                var badMetadataWriter = new BigEndianBinaryWriter(badMetadataStream);
                badMetadataWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badMetadataWriter.Write(VersionOne);
                badMetadataWriter.Write((byte)GlyphMode.None);
                badMetadataWriter.Write((byte)ColorMode.FourBit);
                badMetadataWriter.Write(badMetadata);
                badMetadataStream.Position = 0;
                var badMetadataReader = new BigEndianBinaryReader(badMetadataStream);

                var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(badMetadataReader));
                Assert.That(exception.Message, Is.EqualTo($"Unknown metadata mode '{badMetadata}'."));
            }
        }

        [TestCase((ushort)0, (ushort)0)]
        [TestCase((ushort)1, (ushort)1)]
        [TestCase((ushort)32, (ushort)32)]
        [TestCase(ushort.MaxValue, ushort.MinValue)]
        [TestCase(ushort.MinValue, ushort.MaxValue)]
        [TestCase(ushort.MaxValue, ushort.MaxValue)]
        public void TestLoadingHeader(ushort width, ushort height)
        {
            var stream = new MemoryStream();
            var writer = new BigEndianBinaryWriter(stream);
            writer.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            writer.Write(VersionOne);
            writer.Write((byte)GlyphMode.Palette);
            writer.Write((byte)ColorMode.FourBit);
            writer.Write((byte)MetadataMode.SixteenBit);
            writer.WriteBigEndian(width);
            writer.WriteBigEndian(height);
            writer.Write((byte)32);
            writer.Write((byte)15);
            writer.Write((byte)1);
            writer.Write(Encoding.ASCII.GetBytes("GLY_PAL"));
            writer.Write(Encoding.ASCII.GetBytes("COL_PAL"));

            stream.Position = 0;
            var reader = new BigEndianBinaryReader(stream);
            var header = ImageLoader.LoadHeader(reader);

            Assert.That(header.Signature, Is.EqualTo("NURUIMG"));
            Assert.That(header.Version, Is.EqualTo(1));
            Assert.That(header.GlyphMode, Is.EqualTo(GlyphMode.Palette));
            Assert.That(header.ColorMode, Is.EqualTo(ColorMode.FourBit));
            Assert.That(header.MetadataMode, Is.EqualTo(MetadataMode.SixteenBit));
            Assert.That(header.Width, Is.EqualTo(width));
            Assert.That(header.Height, Is.EqualTo(height));
            Assert.That(header.KeyGlyph, Is.EqualTo(32));
            Assert.That(header.KeyForeground, Is.EqualTo(15));
            Assert.That(header.KeyBackground, Is.EqualTo(1));
            Assert.That(header.GlyphPaletteName, Is.EqualTo("GLY_PAL"));
            Assert.That(header.ColorPaletteName, Is.EqualTo("COL_PAL"));
        }

        [Test]
        public void TestLoadingBrokenHeader()
        {
            var stream = new MemoryStream();
            var writer = new BigEndianBinaryWriter(stream);
            writer.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            writer.Write(VersionOne);
            writer.Write((byte)GlyphMode.Palette);
            writer.Write((byte)ColorMode.FourBit);
            writer.Write((byte)MetadataMode.SixteenBit);
            writer.Write((byte)0); // Should be a ushort width here, but faking broken file.
            stream.Position = 0;
            var reader = new BigEndianBinaryReader(stream);
            
            var exception = Assert.Throws<ImageLoadException>(() => ImageLoader.LoadHeader(reader));
            Assert.That(exception.Message, Is.EqualTo("Could not read stream."));
            Assert.That(exception.InnerException.GetType(), Is.EqualTo(typeof(EndOfStreamException)));
        }

        [Test]
        public void TestLoadingEmptyImage()
        {
            var stream = new MemoryStream();
            var writer = new BigEndianBinaryWriter(stream);
            writer.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            writer.Write(VersionOne);
            writer.Write((byte)GlyphMode.ASCII);
            writer.Write((byte)ColorMode.FourBit);
            writer.Write((byte)MetadataMode.None);
            writer.WriteBigEndian((ushort)0); // width
            writer.WriteBigEndian((ushort)0); // height
            writer.Write((byte)32);
            writer.Write((byte)15);
            writer.Write((byte)1);
            writer.Write(Encoding.ASCII.GetBytes("GLY_PAL"));
            writer.Write(Encoding.ASCII.GetBytes("COL_PAL"));
            // image follows, but it's 0 by 0 characters, so should return empty image.
            stream.Position = 0;

            Image image = ImageLoader.LoadImage(new BigEndianBinaryReader(stream));

            Assert.That(image.Width, Is.EqualTo(0));
            Assert.That(image.Height, Is.EqualTo(0));
            Assert.That(image.GlyphMode, Is.EqualTo(GlyphMode.ASCII));
            Assert.That(image.ColorMode, Is.EqualTo(ColorMode.FourBit));
            Assert.That(image.MetadataMode, Is.EqualTo(MetadataMode.None));
            Assert.That(image.GlyphPalette, Is.EqualTo("GLY_PAL"));
            Assert.That(image.ColorPalette, Is.EqualTo("COL_PAL"));
            var exception = Assert.Throws<IndexOutOfRangeException>(() => image.GetCell(0, 0));
            Assert.That(exception.Message, Is.EqualTo("Attempting to access cell outside bounds."));
        }

        [Test]
        public void TestLoading1pxImage()
        {
            var stream = new MemoryStream();
            var writer = new BigEndianBinaryWriter(stream);
            writer.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            writer.Write(VersionOne);
            writer.Write((byte)GlyphMode.ASCII);
            writer.Write((byte)ColorMode.FourBit);
            writer.Write((byte)MetadataMode.SixteenBit);
            writer.WriteBigEndian((ushort)1); // width
            writer.WriteBigEndian((ushort)1); // height
            writer.Write((byte)32);
            writer.Write((byte)15);
            writer.Write((byte)1);
            writer.Write(Encoding.ASCII.GetBytes("GLY_PAL"));
            writer.Write(Encoding.ASCII.GetBytes("COL_PAL"));
            // image follows:
            Cell expectedCell = new Cell('A', 2, 4, 22334);
            writer.Write(expectedCell.PackCharacter());
            writer.Write(expectedCell.PackColors());
            writer.WriteBigEndian(expectedCell.Metadata);

            stream.Position = 0;

            Image image = ImageLoader.LoadImage(new BigEndianBinaryReader(stream));

            Assert.That(image.Width, Is.EqualTo(1));
            Assert.That(image.Height, Is.EqualTo(1));
            Assert.That(image.GetCell(0, 0).Character, Is.EqualTo(expectedCell.Character));
            Assert.That(image.GetCell(0, 0).Colors.Foreground, Is.EqualTo(expectedCell.Colors.Foreground));
            Assert.That(image.GetCell(0, 0).Colors.Background, Is.EqualTo(expectedCell.Colors.Background));
            Assert.That(image.GetCell(0, 0).Metadata, Is.EqualTo(expectedCell.Metadata));
        }
    }
}