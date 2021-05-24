namespace nuru.NUI
{
    public struct Header
    {
        public string Signature;
        public byte Version;
        public GlyphMode GlyphMode;
        public ColorMode ColorMode;
        public MetadataMode MetadataMode;
        public ushort Width;
        public ushort Height;
        public byte KeyGlyph;
        public byte KeyForeground;
        public byte KeyBackground;
        public string GlyphPaletteName;
        public string ColorPaletteName;
    }
}
