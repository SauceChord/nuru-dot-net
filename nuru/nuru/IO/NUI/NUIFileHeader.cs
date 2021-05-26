using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            NUIFileHeader other = obj as NUIFileHeader;
            
            if (other == null)
                return false;

            return Version == other.Version
                && GlyphMode == other.GlyphMode
                && ColorMode == other.ColorMode
                && MetadataMode == other.MetadataMode
                && Width == other.Width
                && Height == other.Height
                && KeyGlyph == other.KeyGlyph
                && KeyForeground == other.KeyForeground
                && KeyBackground == other.KeyBackground
                && GlyphPaletteName == other.GlyphPaletteName
                && ColorPaletteName == other.ColorPaletteName;
        }

        public override int GetHashCode()
        {
            int hashCode = 850716638;
            hashCode = hashCode * -1521134295 + Version.GetHashCode();
            hashCode = hashCode * -1521134295 + GlyphMode.GetHashCode();
            hashCode = hashCode * -1521134295 + ColorMode.GetHashCode();
            hashCode = hashCode * -1521134295 + MetadataMode.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + KeyGlyph.GetHashCode();
            hashCode = hashCode * -1521134295 + KeyForeground.GetHashCode();
            hashCode = hashCode * -1521134295 + KeyBackground.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GlyphPaletteName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ColorPaletteName);
            return hashCode;
        }
    }
}
