using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryCellReader
    {
        public CellMode Mode { get; protected set; }
        public readonly Stream BaseStream;

        protected BinaryGlyphReader glyphReader;
        protected BinaryColorPairReader colorReader;
        protected BinaryMetadataReader metadataReader;
        protected Func<char> readGlyph;
        protected Func<ColorPair> readColors;
        protected Func<ushort> readMetadata;


        public BinaryCellReader(Stream stream, CellMode cellMode)
        {
            BaseStream = stream;
            Mode = cellMode;
            glyphReader = new BinaryGlyphReader(stream);
            colorReader = new BinaryColorPairReader(stream);
            metadataReader = new BinaryMetadataReader(stream);
            readGlyph = SelectGlyphReader();
            readColors = SelectColorPairReader();
            readMetadata = SelectMetadataReader();
        }

        public Cell ReadCell()
        {
            return new Cell(readGlyph(), readColors(), readMetadata());
        }

        private Func<char> SelectGlyphReader()
        {
            switch (Mode.Glyph)
            {
                case GlyphMode.None: return () => ' ';
                case GlyphMode.ASCII: return glyphReader.ReadASCII;
                case GlyphMode.UTF16: return glyphReader.ReadUTF16;
                case GlyphMode.Palette:
                default:
                    throw new NotSupportedException($"Unsupported glyph mode '{Mode.Glyph}'.");
            }
        }

        private Func<ColorPair> SelectColorPairReader()
        {
            switch (Mode.Color)
            {
                case ColorMode.None: return () => new ColorPair(15, 0);
                case ColorMode.FourBit: return colorReader.Read4BitsPerChannel;
                case ColorMode.EightBit: return colorReader.Read8BitsPerChannel;
                case ColorMode.Palette:
                default:
                    throw new NotSupportedException($"Unsupported color mode '{Mode.Color}'.");
            }
        }

        private Func<ushort> SelectMetadataReader()
        {
            switch (Mode.Metadata)
            {
                case MetadataMode.None: return () => 0;
                case MetadataMode.EightBit: return metadataReader.Read8Bits;
                case MetadataMode.SixteenBit: return metadataReader.Read16Bits;
                default:
                    throw new NotSupportedException($"Unsupported metadata mode '{Mode.Metadata}'.");
            }
        }
    }
}
