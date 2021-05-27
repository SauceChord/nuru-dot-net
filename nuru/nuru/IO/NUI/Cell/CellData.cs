using nuru.IO.NUI.Cell.Color;

namespace nuru.IO.NUI.Cell
{
    public struct CellData
    {
        public char Glyph;
        public ColorData Color;
        public ushort Metadata;

        public CellData(char glyph, ColorData color, ushort metadata) : this()
        {
            Glyph = glyph;
            Color = color;
            Metadata = metadata;
        }

        public CellData(char glyph, byte foreground, byte background, ushort metadata) : this()
        {
            Glyph = glyph;
            Color.Foreground = foreground;
            Color.Background = background;
            Metadata = metadata;
        }
    }
}
