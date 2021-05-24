using System;
using System.Collections.Generic;
using System.Text;

namespace nuru.NUI
{
    public struct CellMode
    {
        public GlyphMode Glyph;
        public ColorMode Color;
        public MetadataMode Metadata;

        public CellMode(GlyphMode glyph, ColorMode color, MetadataMode metadata) : this()
        {
            Glyph = glyph;
            Color = color;
            Metadata = metadata;
        }
    }
}
