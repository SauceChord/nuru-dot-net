using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryMetadataReader
    {
        public BinaryReader Reader;

        public BinaryMetadataReader(Stream stream)
        {
            Reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);
        }

        public ushort Read8Bits()
        {
            return Reader.ReadByte();
        }

        public ushort Read16Bits()
        {
            return Reader.ReadUInt16();
        }
    }
}
