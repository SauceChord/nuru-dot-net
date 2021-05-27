namespace nuru.IO.NUI.Cell.Color
{
    public struct ColorData
    {
        public byte Foreground;
        public byte Background;

        public ColorData(byte foreground, byte background) : this()
        {
            Foreground = foreground;
            Background = background;
        }

        public override string ToString()
        {
            return $"{Foreground}, {Background}";
        }
    }
}
