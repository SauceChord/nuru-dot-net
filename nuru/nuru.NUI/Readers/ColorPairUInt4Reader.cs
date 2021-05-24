using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI.Readers
{
    public class ColorPairUInt4Reader : ReaderBase, IColorPairReader
    {
        public ColorPairUInt4Reader(Stream stream) : base(stream)
        {
        }

        public ColorPair Read()
        {
            int b = reader.ReadByte();
            return new ColorPair((byte)(b >> 4), (byte)(b & 0xf));
        }
    }
}
