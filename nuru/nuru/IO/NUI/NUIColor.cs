namespace nuru.IO.NUI
{
    public struct NUIColor
    {
        public byte Foreground;
        public byte Background;

        public NUIColor(byte foreground, byte background) : this()
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
