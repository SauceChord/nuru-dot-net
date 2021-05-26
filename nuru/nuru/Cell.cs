using System;

namespace nuru
{
    public struct Cell
    {
        public char Glyph;
        public Color Color;
        public ushort Metadata;

        public Cell(char glyph, Color color, ushort metadata) : this()
        {
            Glyph = glyph;
            Color = color;
            Metadata = metadata;
        }

        public Cell(char glyph, byte foreground, byte background, ushort metadata) : this()
        {
            Glyph = glyph;
            Color.Foreground = foreground;
            Color.Background = background;
            Metadata = metadata;
        }
    }
}
