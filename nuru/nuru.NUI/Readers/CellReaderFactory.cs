using System;
using System.Collections.Generic;

namespace nuru.NUI.Readers
{
    public class CellReaderFactory
    {
        Dictionary<GlyphMode, IGlyphReader> glyphReaders = new Dictionary<GlyphMode, IGlyphReader>();
        Dictionary<ColorMode, IColorPairReader> colorPairReaders = new Dictionary<ColorMode, IColorPairReader>();
        Dictionary<MetadataMode, IMetadataReader> metadataReaders = new Dictionary<MetadataMode, IMetadataReader>();

        public CellReader Build(CellMode cellMode)
        {
            if (!glyphReaders.ContainsKey(cellMode.Glyph))
                throw new NoReaderException($"No registered glyph reader that handles '{cellMode.Glyph}'.");

            if (!colorPairReaders.ContainsKey(cellMode.Color))
                throw new NoReaderException($"No registered color reader that handles '{cellMode.Color}'.");

            if (!metadataReaders.ContainsKey(cellMode.Metadata))
                throw new NoReaderException($"No registered metadata reader that handles '{cellMode.Metadata}'.");

            return new CellReader(
                glyphReaders[cellMode.Glyph],
                colorPairReaders[cellMode.Color],
                metadataReaders[cellMode.Metadata]);
        }

        public void RegisterGlyphReader(GlyphMode glyphMode, IGlyphReader glyphReader)
        {
            if (glyphReader == null)
                throw new ArgumentNullException("glyphReader");

            glyphReaders.Add(glyphMode, glyphReader);
        }

        public void RegisterColorPairReader(ColorMode colorMode, IColorPairReader colorPairReader)
        {
            if (colorPairReader == null)
                throw new ArgumentNullException("colorPairReader");

            colorPairReaders.Add(colorMode, colorPairReader);
        }

        public void RegisterMetadataReader(MetadataMode metadataMode, IMetadataReader metadataReader)
        {
            if (metadataReader == null)
                throw new ArgumentNullException("metadataReader");

            metadataReaders.Add(metadataMode, metadataReader);
        }
    }
}
