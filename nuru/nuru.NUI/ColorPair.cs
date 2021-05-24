namespace nuru.NUI
{
    public struct ColorPair
    {
        public byte Foreground;
        public byte Background;

        public ColorPair(byte foreground, byte background) : this()
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
