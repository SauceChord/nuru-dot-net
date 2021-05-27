using nuru.IO.NUI.Cell.Color;
using nuru.IO.NUI.Cell.Glyph;
using nuru.IO.NUI.Cell.Metadata;
using System;
using System.Collections.Generic;

namespace nuru.IO.NUI.Cell
{
    public class CellWriterFactory
    {
        readonly Dictionary<GlyphMode, IGlyphWriter> glyphWriters = new Dictionary<GlyphMode, IGlyphWriter>();
        readonly Dictionary<ColorMode, IColorWriter> colorWriters = new Dictionary<ColorMode, IColorWriter>();
        readonly Dictionary<MetadataMode, IMetadataWriter> metadataWriters = new Dictionary<MetadataMode, IMetadataWriter>();

        public CellWriter Build(CellConfig cellConfig)
        {
            if (!glyphWriters.ContainsKey(cellConfig.Glyph))
                throw new CellFactoryException($"No registered glyph writer that handles '{cellConfig.Glyph}'.");

            if (!colorWriters.ContainsKey(cellConfig.Color))
                throw new CellFactoryException($"No registered color writer that handles '{cellConfig.Color}'.");

            if (!metadataWriters.ContainsKey(cellConfig.Metadata))
                throw new CellFactoryException($"No registered metadata writer that handles '{cellConfig.Metadata}'.");

            return new CellWriter(
                glyphWriters[cellConfig.Glyph],
                colorWriters[cellConfig.Color],
                metadataWriters[cellConfig.Metadata]);
        }

        public void RegisterGlyphWriter(GlyphMode glyphMode, IGlyphWriter glyphWriter)
        {
            if (glyphWriter == null)
                throw new ArgumentNullException("glyphWriter");

            glyphWriters.Add(glyphMode, glyphWriter);
        }

        public void RegisterColorWriter(ColorMode colorMode, IColorWriter colorWriter)
        {
            if (colorWriter == null)
                throw new ArgumentNullException("colorWriter");

            colorWriters.Add(colorMode, colorWriter);
        }

        public void RegisterMetadataWriter(MetadataMode metadataMode, IMetadataWriter metadataWriter)
        {
            if (metadataWriter == null)
                throw new ArgumentNullException("metadataWriter");

            metadataWriters.Add(metadataMode, metadataWriter);
        }
    }
}
