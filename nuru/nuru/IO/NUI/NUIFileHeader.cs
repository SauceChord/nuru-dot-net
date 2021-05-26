using System.IO;

namespace nuru.IO
{
    public class NUIFileHeader
    {
        public const string Signature = "NURUIMG";

        public byte Version;
        public GlyphMode GlyphMode;
        public ColorMode ColorMode;
        public MetadataMode MetadataMode;
        public ushort Width;
        public ushort Height;
        public byte KeyGlyph;
        public byte KeyForeground;
        public byte KeyBackground;
        public string GlyphPaletteName;
        public string ColorPaletteName;

        public static NUIFileHeader FromStream(Stream stream)
        {
            var format = new FormatReader(stream);
            var signature = format.ReadString();

            if (signature != Signature)
                throw new FormatException("Bad header signature");

            return new NUIFileHeader()
            {
                Version = format.ReadUInt8(),
                GlyphMode = format.ReadGlyphMode(),
                ColorMode = format.ReadColorMode(),
                MetadataMode = format.ReadMetadataMode(),
                Width = format.ReadUInt16(),
                Height = format.ReadUInt16(),
                KeyGlyph = format.ReadUInt8(),
                KeyForeground = format.ReadUInt8(),
                KeyBackground = format.ReadUInt8(),
                GlyphPaletteName = format.ReadString(),
                ColorPaletteName = format.ReadString(),
            };
        }

        public void ToStream(Stream stream)
        {
            var format = new FormatWriter(stream);
            format.Write(Signature);
            format.Write(Version);
            format.Write(GlyphMode);
            format.Write(ColorMode);
            format.Write(MetadataMode);
            format.Write(Width);
            format.Write(Height);
            format.Write(KeyGlyph);
            format.Write(KeyForeground);
            format.Write(KeyBackground);
            format.Write(GlyphPaletteName);
            format.Write(ColorPaletteName);
        }

        public CellConfig GetCellConfig()
        {
            return new CellConfig(GlyphMode, ColorMode, MetadataMode);
        }
    }
}
