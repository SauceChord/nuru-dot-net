namespace nuru.Palette
{
    public static class ANSI4
    {
        // https://en.wikipedia.org/wiki/ANSI_escape_code#3-bit_and_4-bit
        static readonly int[] rawRGB = {
            0x000000, 0xcd0000, 0x00cd00, 0xcdcd00, 0x0000ee, 0xcd00cd, 0x00cdcd, 0xe5e5e5,
            0x7f7f7f, 0xff0000, 0x00ff00, 0xffff00, 0x5c5cff, 0xff00ff, 0x00ffff, 0xffffff
        };

        public static RGB ToRGB(byte ansi4)
        {
            return new RGB(rawRGB[ansi4]);
        }
    }
}
