using System;
using System.IO;
using System.Text;
using nuru.NUI.Readers;

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

            IGlyphReader glyphReader = GetGlyphReader(header.GlyphMode, reader.BaseStream);
            IColorPairReader colorReader = GetColorReader(header.ColorMode, reader.BaseStream);
            IMetadataReader metadataReader = GetMetadataReader(header.MetadataMode, reader.BaseStream);
            CellReader cellReader = new CellReader(glyphReader, colorReader, metadataReader);

            for (ushort row = 0; row < image.Height; ++row)
                for (ushort col = 0; col < image.Width; ++col)
                    cells[col, row] = cellReader.Read();

            image.Cells = cells;

            return image;
        }

        private static IMetadataReader GetMetadataReader(MetadataMode mode, Stream stream)
        {
            switch (mode)
            {
                case MetadataMode.None: return new MetadataVoidReader();
                case MetadataMode.EightBit: return new MetadataUInt8Reader(stream);
                case MetadataMode.SixteenBit: return new MetadataUInt16Reader(stream);
                default:
                    throw new InvalidOperationException();
            }
        }

        private static IColorPairReader GetColorReader(ColorMode mode, Stream stream)
        {
            switch (mode)
            {
                case ColorMode.None: return new ColorPairVoidReader();
                case ColorMode.FourBit: return new ColorPairUInt4Reader(stream);
                case ColorMode.EightBit: return new ColorPairUInt8Reader(stream);
                case ColorMode.Palette: return new ColorPairUInt8Reader(stream);
                default:
                    throw new InvalidOperationException();
            }
        }

        private static IGlyphReader GetGlyphReader(GlyphMode mode, Stream stream)
        {
            switch (mode)
            {
                case GlyphMode.None: return new GlyphSpaceReader();
                case GlyphMode.ASCII: return new GlyphASCIIReader(stream);
                case GlyphMode.UTF16: return new GlyphUnicodeReader(stream);
                case GlyphMode.Palette: return new GlyphASCIIReader(stream);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
