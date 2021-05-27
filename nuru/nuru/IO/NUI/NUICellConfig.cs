namespace nuru.IO.NUI
{
    public struct NUICellConfig
    {
        public GlyphMode Glyph;
        public ColorMode Color;
        public MetadataMode Metadata;

        public NUICellConfig(GlyphMode glyph, ColorMode color, MetadataMode metadata) : this()
        {
            Glyph = glyph;
            Color = color;
            Metadata = metadata;
        }
    }
}
