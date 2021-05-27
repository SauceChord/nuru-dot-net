namespace nuru.IO.NUI
{
    public struct NUICell
    {
        public char Glyph;
        public NUIColor Color;
        public ushort Metadata;

        public NUICell(char glyph, NUIColor color, ushort metadata) : this()
        {
            Glyph = glyph;
            Color = color;
            Metadata = metadata;
        }

        public NUICell(char glyph, byte foreground, byte background, ushort metadata) : this()
        {
            Glyph = glyph;
            Color.Foreground = foreground;
            Color.Background = background;
            Metadata = metadata;
        }
    }
}
