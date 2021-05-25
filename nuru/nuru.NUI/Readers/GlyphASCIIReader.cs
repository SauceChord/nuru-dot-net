using BigEndian.IO;
using System;

namespace nuru.NUI.Readers
{
    public class GlyphASCIIReader : IGlyphReader
    {
        public char Read(BigEndianBinaryReader reader)
        {
            return Convert.ToChar(reader.ReadByte());
        }
    }
}
