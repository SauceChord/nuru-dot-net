using System;
using System.Collections.Generic;
using System.Text;

namespace nuru.NUI.Writers
{
    public class CellWriter
    {
        protected IGlyphWriter glyph;
        protected IColorPairWriter color;
        protected IMetadataWriter metadata;

        public CellWriter(IGlyphWriter glyph, IColorPairWriter color, IMetadataWriter meta)
        {
            this.glyph = glyph;
            this.color = color;
            this.metadata = meta;
        }

        public void Write(Cell cell)
        {
            glyph.Write(cell.Character);
            color.Write(cell.Colors);
            metadata.Write(cell.Metadata);
        }
    }
}
