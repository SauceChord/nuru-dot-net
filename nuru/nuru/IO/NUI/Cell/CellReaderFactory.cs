using System;
using System.Collections.Generic;

namespace nuru.IO.NUI
{
    public class CellReaderFactory
    {
        readonly Dictionary<GlyphMode, IGlyphReader> glyphReaders = new Dictionary<GlyphMode, IGlyphReader>();
        readonly Dictionary<ColorMode, IColorReader> colorPairReaders = new Dictionary<ColorMode, IColorReader>();
        readonly Dictionary<MetadataMode, IMetadataReader> metadataReaders = new Dictionary<MetadataMode, IMetadataReader>();

        public CellReader Build(CellConfig cellConfig)
        {
            if (!glyphReaders.ContainsKey(cellConfig.Glyph))
                throw new CellFactoryException($"No registered glyph reader that handles '{cellConfig.Glyph}'.");

            if (!colorPairReaders.ContainsKey(cellConfig.Color))
                throw new CellFactoryException($"No registered color reader that handles '{cellConfig.Color}'.");

            if (!metadataReaders.ContainsKey(cellConfig.Metadata))
                throw new CellFactoryException($"No registered metadata reader that handles '{cellConfig.Metadata}'.");

            return new CellReader(
                glyphReaders[cellConfig.Glyph],
                colorPairReaders[cellConfig.Color],
                metadataReaders[cellConfig.Metadata]);
        }

        public void RegisterGlyphReader(GlyphMode glyphMode, IGlyphReader glyphReader)
        {
            if (glyphReader == null)
                throw new ArgumentNullException("glyphReader");

            glyphReaders.Add(glyphMode, glyphReader);
        }

        public void RegisterColorReader(ColorMode colorMode, IColorReader colorReader)
        {
            if (colorReader == null)
                throw new ArgumentNullException("colorReader");

            colorPairReaders.Add(colorMode, colorReader);
        }

        public void RegisterMetadataReader(MetadataMode metadataMode, IMetadataReader metadataReader)
        {
            if (metadataReader == null)
                throw new ArgumentNullException("metadataReader");

            metadataReaders.Add(metadataMode, metadataReader);
        }
    }
}
