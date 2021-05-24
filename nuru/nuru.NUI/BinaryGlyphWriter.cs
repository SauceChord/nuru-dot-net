using System;
using System.IO;
using System.Text;

namespace nuru.NUI
{
    public class BinaryGlyphWriter
    {
        public BinaryWriter Writer;

        public BinaryGlyphWriter(Stream stream)
        {
            Writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
        }

        public void WriteASCII(char c)
        {
            Writer.Write(Convert.ToByte(c));
        }

        public void WriteUTF16(char c)
        {
            Writer.Write(c);
        }
    }
}
