using System;
using System.IO;

namespace nuru.NUI.Readers
{
    public class GlyphASCIIReader : ReaderBase, IGlyphReader
    {
        public GlyphASCIIReader(Stream stream) : base(stream)
        {
        }

        public char Read()
        {
            return Convert.ToChar(reader.ReadByte());
        }
    }
}
