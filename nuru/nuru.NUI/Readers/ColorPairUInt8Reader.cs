using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI.Readers
{
    public class ColorPairUInt8Reader : ReaderBase, IColorPairReader
    {
        public ColorPairUInt8Reader(Stream stream) : base(stream)
        {
        }

        public ColorPair Read()
        {
            byte background = reader.ReadByte();
            byte foreground = reader.ReadByte();
            return new ColorPair(foreground, background);
        }
    }
}
