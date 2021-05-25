using System;
using System.IO;
using System.Text;
using BigEndian.IO;
using nuru.NUI.Readers;

namespace nuru.NUI
{
    public class ImageLoader
    {
        public static Header LoadHeader(BigEndianBinaryReader reader)
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

                header.Width = reader.ReadBigEndianUInt16();
                header.Height = reader.ReadBigEndianUInt16();
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


        public static Image LoadImage(BigEndianBinaryReader reader)
        {
            Header header = LoadHeader(reader);
            Image image = new Image
            {
                Width = header.Width,
                Height = header.Height,
                GlyphMode = header.GlyphMode,
                ColorMode = header.ColorMode,
                MetadataMode = header.MetadataMode,
                GlyphPalette = header.GlyphPaletteName,
                ColorPalette = header.ColorPaletteName
            };


            CellReaderFactory cellReaderFactory = new CellReaderFactory();
            
            cellReaderFactory.RegisterGlyphReader(GlyphMode.None, new GlyphSpaceReader());
            cellReaderFactory.RegisterGlyphReader(GlyphMode.ASCII, new GlyphASCIIReader());
            cellReaderFactory.RegisterGlyphReader(GlyphMode.UTF16, new GlyphUnicodeReader());
            cellReaderFactory.RegisterGlyphReader(GlyphMode.Palette, new GlyphASCIIReader());

            cellReaderFactory.RegisterColorPairReader(ColorMode.None, new ColorPairVoidReader());
            cellReaderFactory.RegisterColorPairReader(ColorMode.FourBit, new ColorPairUInt4Reader());
            cellReaderFactory.RegisterColorPairReader(ColorMode.EightBit, new ColorPairUInt8Reader());
            cellReaderFactory.RegisterColorPairReader(ColorMode.Palette, new ColorPairUInt8Reader());

            cellReaderFactory.RegisterMetadataReader(MetadataMode.None, new MetadataVoidReader());
            cellReaderFactory.RegisterMetadataReader(MetadataMode.EightBit, new MetadataUInt8Reader());
            cellReaderFactory.RegisterMetadataReader(MetadataMode.SixteenBit, new MetadataUInt16Reader());

            var cellMode = new CellMode()
            {
                Glyph = header.GlyphMode,
                Color = header.ColorMode,
                Metadata = header.MetadataMode
            };

            var cellReader = cellReaderFactory.Build(cellMode);

            Cell[,] cells = new Cell[image.Width, image.Height];
            for (ushort row = 0; row < image.Height; ++row)
                for (ushort col = 0; col < image.Width; ++col)
                    cells[col, row] = cellReader.Read(reader);
            image.Cells = cells;

            return image;
        }
    }
}
