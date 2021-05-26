using System;
using System.Collections.Generic;
using System.Text;

namespace nuru
{
    public struct RGB
    {
        public byte R;
        public byte G;
        public byte B;

        public RGB(int rgb)
        {
            R = (byte)((rgb & 0x00ff0000) >> 16);
            G = (byte)((rgb & 0x0000ff00) >> 8);
            B = (byte)((rgb & 0x000000ff) >> 0);
        }

        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
