using System;
using System.IO;
using System.Linq;
using System.Text;

namespace nuru.NUI
{
    public class ImageLoader
    {
        public static ImageHeader LoadHeader(BinaryReader reader)
        {
            try
            {
                if (reader == null)
                    throw new System.ArgumentNullException("reader");

                ImageHeader header = new ImageHeader();
                header.Signature = Encoding.ASCII.GetString(reader.ReadBytes(7));

                if (header.Signature != "NURUIMG")
                    throw new ImageLoadException("No valid NUI signature found.");

                header.Version = reader.ReadByte();

                if (header.Version != 1)
                    throw new ImageLoadException($"Unsupported version '{header.Version}'.");

                header.GlyphMode = (GlyphMode)reader.ReadByte();

                if (IsIllegalEnum(header.GlyphMode))
                    throw new ImageLoadException($"Unknown glyph mode '{header.GlyphMode}'.");

                header.ColorMode = (ColorMode)reader.ReadByte();

                if (IsIllegalEnum(header.ColorMode))
                    throw new ImageLoadException($"Unknown color mode '{header.ColorMode}'.");

                if (header.GlyphMode == GlyphMode.None
                 && header.ColorMode == ColorMode.None)
                    throw new ImageLoadException("Color mode can't be None (0) when glyph mode is set to None (0).");

                header.MetadataMode = (MetadataMode)reader.ReadByte();

                if (IsIllegalEnum(header.MetadataMode))
                    throw new ImageLoadException($"Unknown metadata mode '{header.MetadataMode}'.");

                header.Width = reader.ReadUInt16();
                header.Height = reader.ReadUInt16();
                header.KeyGlyph = reader.ReadByte();
                header.KeyForeground = reader.ReadByte();
                header.KeyBackground = reader.ReadByte();
                header.GlyphPaletteName = Encoding.ASCII.GetString(reader.ReadBytes(7));
                header.ColorPaletteName = Encoding.ASCII.GetString(reader.ReadBytes(7));

                return header;
            }
            catch (EndOfStreamException e)
            {
                throw new ImageLoadException("Could not read stream.", e);
            }
        }
        private static bool IsIllegalEnum<T>(T value)
        {
            return !((T[])Enum.GetValues(typeof(T))).Contains(value);
        }
    }
}
