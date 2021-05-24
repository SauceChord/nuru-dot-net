using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryCellWriter
    {
        public CellMode Mode { get; protected set; }
        public readonly Stream BaseStream;

        protected BinaryGlyphWriter glyphWriter;
        protected BinaryColorPairWriter colorWriter;
        protected BinaryMetadataWriter metadataWriter;
        protected Action<char> writeGlyph;
        protected Action<ColorPair> writeColors;
        protected Action<ushort> writeMetadata;


        public BinaryCellWriter(Stream stream, CellMode cellMode)
        {
            BaseStream = stream;
            Mode = cellMode;
            glyphWriter = new BinaryGlyphWriter(stream);
            colorWriter = new BinaryColorPairWriter(stream);
            metadataWriter = new BinaryMetadataWriter(stream);
            writeGlyph = SelectGlyphWriter();
            writeColors = SelectColorPairWriter();
            writeMetadata = SelectMetadataWriter();
        }

        public void WriteCell(Cell cell)
        {
            writeGlyph(cell.Character);
            writeColors(cell.Colors);
            writeMetadata(cell.Metadata);
        }

        private Action<char> SelectGlyphWriter()
        {
            switch (Mode.Glyph)
            {
                case GlyphMode.None: return (c) => { };
                case GlyphMode.ASCII: return glyphWriter.WriteASCII;
                case GlyphMode.UTF16: return glyphWriter.WriteUTF16;
                case GlyphMode.Palette:
                default:
                    throw new NotSupportedException($"Unsupported glyph mode '{Mode.Glyph}'.");
            }
        }

        private Action<ColorPair> SelectColorPairWriter()
        {
            switch (Mode.Color)
            {
                case ColorMode.None: return (c) => { };
                case ColorMode.FourBit: return colorWriter.Write4BitsPerChannel;
                case ColorMode.EightBit: return colorWriter.Write8BitsPerChannel;
                case ColorMode.Palette:
                default:
                    throw new NotSupportedException($"Unsupported color mode '{Mode.Color}'.");
            }
        }

        private Action<ushort> SelectMetadataWriter()
        {
            switch (Mode.Metadata)
            {
                case MetadataMode.None: return (c) => { };
                case MetadataMode.EightBit: return metadataWriter.Write8Bits;
                case MetadataMode.SixteenBit: return metadataWriter.Write16Bits;
                default:
                    throw new NotSupportedException($"Unsupported metadata mode '{Mode.Metadata}'.");
            }
        }
    }
}
