using System;

namespace nuru.NUI
{
    public struct Cell
    {
        public char Character;
        public ColorPair Colors;
        public ushort Metadata;

        public Cell(char character, ColorPair colors, ushort metadata) : this()
        {
            Character = character;
            Colors = colors;
            Metadata = metadata;
        }

        public Cell(char character, byte foreground, byte background, ushort metadata) : this()
        {
            Character = character;
            Colors.Foreground = foreground;
            Colors.Background = background;
            Metadata = metadata;
        }

        public byte PackColors()
        {
            return (byte)((Colors.Foreground & 0x0f) << 4 | (Colors.Background & 0x0f));
        }

        public byte PackCharacter()
        {
            return Convert.ToByte(Character);
        }
    }
}
