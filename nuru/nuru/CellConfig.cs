namespace nuru
{
    public struct CellConfig
    {
        public GlyphMode Glyph;
        public ColorMode Color;
        public MetadataMode Metadata;

        public CellConfig(GlyphMode glyph, ColorMode color, MetadataMode metadata) : this()
        {
            Glyph = glyph;
            Color = color;
            Metadata = metadata;
        }
    }
}
