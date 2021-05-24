using System;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryGlyphReader
    {
        public BinaryReader Reader;

        public BinaryGlyphReader(Stream stream)
        {
            Reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);            
        }

        public char ReadASCII()
        {
            return Convert.ToChar(Reader.ReadByte());
        }

        public char ReadUTF16()
        {
            return Reader.ReadChar();
        }
    }
}
