using BigEndian.IO;
using System;

namespace nuru.IO.NUI
{
    public class GlyphASCIIReader : IGlyphReader
    {
        public virtual char Read(BigEndianBinaryReader reader)
        {
            return Convert.ToChar(reader.ReadByte());
        }
    }
}
