using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryColorPairWriter
    {
        public BinaryWriter Writer;
        public BinaryColorPairWriter(Stream stream)
        {
            Writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
        }

        public void Write4BitsPerChannel(ColorPair pair)
        {
            Writer.Write((byte)((pair.Foreground & 0x0f) << 4 | (pair.Background & 0x0f)));
        }

        public void Write8BitsPerChannel(ColorPair pair)
        {
            Writer.Write(pair.Foreground);
            Writer.Write(pair.Background);
        }
    }
}
