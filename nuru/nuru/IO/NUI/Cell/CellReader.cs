using BigEndian.IO;
using nuru.IO.NUI.Cell.Color;
using nuru.IO.NUI.Cell.Glyph;

namespace nuru.IO.NUI.Cell
{
    public class CellReader
    {
        protected IGlyphReader glyph;
        protected IColorReader color;
        protected IMetadataReader metadata;

        public CellReader(IGlyphReader glyph, IColorReader color, IMetadataReader metadata)
        {
            this.glyph = glyph;
            this.color = color;
            this.metadata = metadata;
        }

        public CellData Read(BigEndianBinaryReader reader)
        {
            return new CellData(glyph.Read(reader), color.Read(reader), metadata.Read(reader));
        }
    }
}
