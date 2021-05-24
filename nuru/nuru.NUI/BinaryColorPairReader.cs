using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryColorPairReader
    {
        public BinaryReader Reader;
        public BinaryColorPairReader(Stream stream)
        {
            Reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);
        }

        public ColorPair Read4BitsPerChannel()
        {
            int b = Reader.ReadByte();
            return new ColorPair((byte)(b >> 4), (byte)(b & 0xf));
        }

        public ColorPair Read8BitsPerChannel()
        {
            return new ColorPair(Reader.ReadByte(), Reader.ReadByte());
        }
    }
}
