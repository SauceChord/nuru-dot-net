namespace nuru.NUI.Readers
{
    public class CellReader
    {
        protected IGlyphReader glyph;
        protected IColorPairReader color;
        protected IMetadataReader metadata;

        public CellReader(IGlyphReader glyph, IColorPairReader color, IMetadataReader metadata)
        {
            this.glyph = glyph;
            this.color = color;
            this.metadata = metadata;
        }

        public Cell Read()
        {
            return new Cell(glyph.Read(), color.Read(), metadata.Read());
        }
    }
}
