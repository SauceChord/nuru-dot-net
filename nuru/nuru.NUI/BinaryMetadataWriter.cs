using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryMetadataWriter
    {
        public BinaryWriter Writer;

        public BinaryMetadataWriter(Stream stream)
        {
            Writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
        }

        public void Write8Bits(ushort value)
        {
            Writer.Write((byte)value);
        }

        public void Write16Bits(ushort value)
        {
            Writer.Write(value);
        }
    }
}
