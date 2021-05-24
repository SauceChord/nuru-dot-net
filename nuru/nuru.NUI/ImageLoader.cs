using System;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class ImageLoader
    {
        public static Header LoadHeader(BinaryReader reader)
        {
            try
            {
                if (reader == null)
                    throw new ArgumentNullException("reader");

                Header header = new Header();
                header.Signature = Encoding.ASCII.GetString(reader.ReadBytes(7));

                if (header.Signature != "NURUIMG")
                    throw new ImageLoadException("No valid NUI signature found.");

                header.Version = reader.ReadByte();

                if (header.Version != 1)
                    throw new ImageLoadException($"Unsupported version '{header.Version}'.");

                header.GlyphMode = (GlyphMode)reader.ReadByte();

                if (header.GlyphMode.IsUndefined())
                    throw new ImageLoadException($"Unknown glyph mode '{header.GlyphMode}'.");

                header.ColorMode = (ColorMode)reader.ReadByte();

                if (header.ColorMode.IsUndefined())
                    throw new ImageLoadException($"Unknown color mode '{header.ColorMode}'.");

                if (header.GlyphMode == GlyphMode.None
                 && header.ColorMode == ColorMode.None)
                    throw new ImageLoadException("Color mode can't be None (0) when glyph mode is set to None (0).");

                header.MetadataMode = (MetadataMode)reader.ReadByte();

                if (header.MetadataMode.IsUndefined())
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


        public static Image LoadImage(BinaryReader reader)
        {
            Header header = LoadHeader(reader);
            Image image = new Image();
            image.Width = header.Width;
            image.Height = header.Height;
            image.GlyphMode = header.GlyphMode;
            image.ColorMode = header.ColorMode;
            image.MetadataMode = header.MetadataMode;
            image.GlyphPalette = header.GlyphPaletteName;
            image.ColorPalette = header.ColorPaletteName;            

            Cell[,] cells = new Cell[image.Width, image.Height];

            CellMode mode = new CellMode(header.GlyphMode, header.ColorMode, header.MetadataMode);
            BinaryCellReader cellReader = new BinaryCellReader(reader.BaseStream, mode);

            for (ushort row = 0; row < image.Height; ++row)
                for (ushort col = 0; col < image.Width; ++col)
                    cells[col, row] = cellReader.ReadCell();

            image.Cells = cells;

            return image;
        }
    }
}
