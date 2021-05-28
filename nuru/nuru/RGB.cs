namespace nuru
{
    public struct RGB
    {
        public byte Red;
        public byte Green;
        public byte Blue;

        public RGB(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public override string ToString()
        {
            return Red.ToString("X2") + Green.ToString("X2") + Blue.ToString("X2");
        }
    }
}
