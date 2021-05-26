using NUnit.Framework;
using nuru.IO.NUI;
using nuru.IO.NUP;
using System.Collections.Generic;

namespace nuru.IO.Integration.Tests
{
    public class NUIFileTests
    {
        const string path_house = "nui/house.nui";
        const string path_nuru_cat = "nui/nuru_cat.nui";
        const string path_nuru_dot_net = "nui/nuru_dot_net.nui";
        const string path_nuru_dot_net_alt = "nui/nuru_dot_net_alt.nui";
        const string path_nuru_header = "nui/nuru_header.nui";
        const string path_nuru_header_cool = "nui/nuru_header_cool.nui";
        const string path_nuru_header_coolest = "nui/nuru_header_coolest.nui";
        const string path_test = "nui/test.nui";
        const string path_test_download = "nui/test_download.nui";
        const string path_togglebit = "nui/togglebit.nui";

        Dictionary<string, NUIFileHeader> expectedHeaders;

        [SetUp]
        public void Setup()
        {
            expectedHeaders = new Dictionary<string, NUIFileHeader>();
            expectedHeaders.Add(path_house,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 9,
                    Height = 4,
                    KeyGlyph = 32,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_nuru_cat,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 64,
                    Height = 11,
                    KeyGlyph = 32,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_nuru_dot_net,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 81,
                    Height = 5,
                    KeyGlyph = 32,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_nuru_dot_net_alt,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 63,
                    Height = 5,
                    KeyGlyph = 32,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_nuru_header,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 78,
                    Height = 12,
                    KeyGlyph = 1,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_nuru_header_cool,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 78,
                    Height = 12,
                    KeyGlyph = 1,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_nuru_header_coolest,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 78,
                    Height = 12,
                    KeyGlyph = 1,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_test,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.ASCII,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 3,
                    Height = 2,
                    KeyGlyph = 32,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "       ", // yes, the file has 7 spaces
                    ColorPaletteName = "       ", // yes, the file has 7 spaces
                });
            expectedHeaders.Add(path_test_download,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 32,
                    Height = 16,
                    KeyGlyph = 32,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "nurustd",
                    ColorPaletteName = "aurora ", // yes, the file has a space in the palette name...
                });
            expectedHeaders.Add(path_togglebit,
                new NUIFileHeader()
                {
                    Version = 1,
                    GlyphMode = GlyphMode.Palette,
                    ColorMode = ColorMode.EightBit,
                    MetadataMode = MetadataMode.None,
                    Width = 14,
                    Height = 7,
                    KeyGlyph = 32,
                    KeyForeground = 15,
                    KeyBackground = 0,
                    GlyphPaletteName = "NURUSTD", // yes yes, capital letters this time.
                    ColorPaletteName = "", // file contained "\0\0\0\0\0\0\0"
                });
        }

        [TestCase(path_house)]
        [TestCase(path_nuru_cat)]
        [TestCase(path_nuru_dot_net)]
        [TestCase(path_nuru_dot_net_alt)]
        [TestCase(path_nuru_header)]
        [TestCase(path_nuru_header_cool)]
        [TestCase(path_nuru_header_coolest)]
        [TestCase(path_test)]
        [TestCase(path_test_download)]
        [TestCase(path_togglebit)]
        public void FromFile_GivenPath_HasCorrectHeader(string path)
        {
            var file = NUIFile.FromFile(path);
            var header = file.Header;
            Assert.That(header, Is.EqualTo(expectedHeaders[path]));
        }
    }
}