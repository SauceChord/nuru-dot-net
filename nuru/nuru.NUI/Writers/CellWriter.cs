using BigEndian.IO;

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

        public void Write(BigEndianBinaryWriter writer, Cell cell)
        {
            glyph.Write(writer, cell.Character);
            color.Write(writer, cell.Colors);
            metadata.Write(writer, cell.Metadata);
        }
    }
}
