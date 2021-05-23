using System.Linq;
using System;
using System.IO;
using System.Text;
using NUnit.Framework;


public enum PaletteType : byte
{
    None = 0,
    ColorEightBit = 1,
    GlyphUnicode = 2,
    ColorRGB = 3,
}

public struct Cell
{
    public char Character;
    public byte Foreground;
    public byte Background;
    public ushort Metadata;
}

namespace nuru.NUI.Tests
{
    public class Tests
    {
        const byte VersionOne = 1;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLoadWithNullReader()
        {
            var exception = Assert.Throws<System.ArgumentNullException>(() => Load(null));
            Assert.That(exception.ParamName, Is.EqualTo("reader"));
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'reader')"));
        }

        [Test]
        public void TestLoadWithEmptyReader()
        {
            var emptyStream = new MemoryStream();
            var emptyReader = new BinaryReader(emptyStream);

            var exception = Assert.Throws<ImageLoadException>(() => Load(emptyReader));
            Assert.That(exception.Message, Is.EqualTo("No valid NUI signature found."));
        }

        [Test]
        public void TestLoadWithBadSignature()
        {
            // Proper signature should be NURUIMG
            // Here we supply it with a zero length string.
            var badSigStream = new MemoryStream(new byte[7]);
            var badSigReader = new BinaryReader(badSigStream);

            var exception = Assert.Throws<ImageLoadException>(() => Load(badSigReader));
            Assert.That(exception.Message, Is.EqualTo("No valid NUI signature found."));
        }

        [TestCase(0, 0)]
        [TestCase(2, 255)]
        public void TestLoadWithUnsupportedVersion(byte badRangeStart, byte badRangeEnd)
        {
            for (int badVersion = badRangeStart; badVersion <= badRangeEnd; ++badVersion)
            {
                var badVersionStream = new MemoryStream();
                var badVersionWriter = new BinaryWriter(badVersionStream);
                badVersionWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badVersionWriter.Write(badVersion);

                badVersionStream.Position = 0;
                var badVersionReader = new BinaryReader(badVersionStream);

                var exception = Assert.Throws<ImageLoadException>(() => Load(badVersionReader));
                Assert.That(exception.Message, Is.EqualTo($"Unsupported version '{badVersion}'."));
            }
        }

        [TestCase(3, 128)]
        [TestCase(130, 255)]
        public void TestLoadWithBadGlyphMode(byte badRangeStart, byte badRangeEnd)
        {
            for (int badGlyph = badRangeStart; badGlyph <= badRangeEnd; ++badGlyph)
            {
                var badGlyphStream = new MemoryStream();
                var badGlyphWriter = new BinaryWriter(badGlyphStream);
                badGlyphWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badGlyphWriter.Write(VersionOne);
                badGlyphWriter.Write(badGlyph);
                badGlyphStream.Position = 0;
                var badGlyphReader = new BinaryReader(badGlyphStream);

                var exception = Assert.Throws<ImageLoadException>(() => Load(badGlyphReader));
                Assert.That(exception.Message, Is.EqualTo($"Unknown glyph mode '{badGlyph}'."));
            }
        }

        [TestCase(3, 129)]
        [TestCase(131, 255)]
        public void TestLoadWithBadColorMode(byte badRangeStart, byte badRangeEnd)
        {
            for (int badColor = badRangeStart; badColor <= badRangeEnd; ++badColor)
            {
                var badColorStream = new MemoryStream();
                var badColorWriter = new BinaryWriter(badColorStream);
                badColorWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badColorWriter.Write(VersionOne);
                badColorWriter.Write((byte)GlyphMode.None);
                badColorWriter.Write(badColor);
                badColorStream.Position = 0;
                var badColorReader = new BinaryReader(badColorStream);

                var exception = Assert.Throws<ImageLoadException>(() => Load(badColorReader));
                Assert.That(exception.Message, Is.EqualTo($"Unknown color mode '{badColor}'."));
            }
        }

        [Test]
        public void TestLoadWithBadGlyphAndColorMode()
        {
            var badColorStream = new MemoryStream();
            var badColorWriter = new BinaryWriter(badColorStream);
            badColorWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            badColorWriter.Write(VersionOne);
            badColorWriter.Write((byte)GlyphMode.None);
            badColorWriter.Write((byte)ColorMode.None); // Docs say cant be none if glyph is none
            badColorStream.Position = 0;
            var badColorReader = new BinaryReader(badColorStream);

            var exception = Assert.Throws<ImageLoadException>(() => Load(badColorReader));
            Assert.That(exception.Message, Is.EqualTo("Color mode can't be None (0) when glyph mode is set to None (0)."));
        }

        [TestCase(3, 255)]
        public void TestLoadWithBadMetadataMode(byte badRangeStart, byte badRangeEnd)
        {
            for (int badMetadata = badRangeStart; badMetadata <= badRangeEnd; ++badMetadata)
            {
                var badMetadataStream = new MemoryStream();
                var badMetadataWriter = new BinaryWriter(badMetadataStream);
                badMetadataWriter.Write(Encoding.ASCII.GetBytes("NURUIMG"));
                badMetadataWriter.Write(VersionOne);
                badMetadataWriter.Write((byte)GlyphMode.None);
                badMetadataWriter.Write((byte)ColorMode.FourBit);
                badMetadataWriter.Write(badMetadata);
                badMetadataStream.Position = 0;
                var badMetadataReader = new BinaryReader(badMetadataStream);

                var exception = Assert.Throws<ImageLoadException>(() => Load(badMetadataReader));
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
            var writer = new BinaryWriter(stream);
            writer.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            writer.Write(VersionOne);
            writer.Write((byte)GlyphMode.Palette);
            writer.Write((byte)ColorMode.FourBit);
            writer.Write((byte)MetadataMode.TwoBytes);
            writer.Write(width);
            writer.Write(height);
            writer.Write((byte)32);
            writer.Write((byte)15);
            writer.Write((byte)1);
            writer.Write(Encoding.ASCII.GetBytes("GLY_PAL"));
            writer.Write(Encoding.ASCII.GetBytes("COL_PAL"));

            stream.Position = 0;
            var reader = new BinaryReader(stream);
            var header = Load(reader);

            Assert.That(header.Signature, Is.EqualTo("NURUIMG"));
            Assert.That(header.Version, Is.EqualTo(1));
            Assert.That(header.GlyphMode, Is.EqualTo(GlyphMode.Palette));
            Assert.That(header.ColorMode, Is.EqualTo(ColorMode.FourBit));
            Assert.That(header.MetadataMode, Is.EqualTo(MetadataMode.TwoBytes));
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
            var writer = new BinaryWriter(stream);
            writer.Write(Encoding.ASCII.GetBytes("NURUIMG"));
            writer.Write(VersionOne);
            writer.Write((byte)GlyphMode.Palette);
            writer.Write((byte)ColorMode.FourBit);
            writer.Write((byte)MetadataMode.TwoBytes);
            writer.Write((byte)0); // Should be a ushort width here, but faking broken file.
            stream.Position = 0;
            var reader = new BinaryReader(stream);
            
            var exception = Assert.Throws<ImageLoadException>(() => Load(reader));
            Assert.That(exception.Message, Is.EqualTo("Could not read stream."));
            Assert.That(exception.InnerException.GetType(), Is.EqualTo(typeof(EndOfStreamException)));
        }

        public ImageHeader Load(BinaryReader reader)
        {
            return ImageLoader.LoadHeader(reader);
        }
    }
}