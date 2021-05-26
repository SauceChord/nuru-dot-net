namespace nuru
{
    public struct Color
    {
        public byte Foreground;
        public byte Background;

        public Color(byte foreground, byte background) : this()
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
