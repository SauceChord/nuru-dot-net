using BigEndian.IO;

namespace nuru.IO.NUI.Cell
{
    public class CellWriter
    {
        protected IGlyphWriter glyph;
        protected IColorWriter color;
        protected IMetadataWriter metadata;

        public CellWriter(IGlyphWriter glyph, IColorWriter color, IMetadataWriter meta)
        {
            this.glyph = glyph;
            this.color = color;
            this.metadata = meta;
        }

        public void Write(BigEndianBinaryWriter writer, CellData cell)
        {
            glyph.Write(writer, cell.Glyph);
            color.Write(writer, cell.Color);
            metadata.Write(writer, cell.Metadata);
        }
    }
}
