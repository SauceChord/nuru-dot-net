using BigEndian.IO;

namespace nuru.IO.NUI
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

        public Cell Read(BigEndianBinaryReader reader)
        {
            return new Cell(glyph.Read(reader), color.Read(reader), metadata.Read(reader));
        }
    }
}
