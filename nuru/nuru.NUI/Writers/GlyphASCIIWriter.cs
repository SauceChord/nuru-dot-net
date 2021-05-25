using BigEndian.IO;
using System;

namespace nuru.NUI.Writers
{
    public class GlyphASCIIWriter : IGlyphWriter
    {
        public void Write(BigEndianBinaryWriter writer, char glyph)
        {
            writer.Write(Convert.ToByte(glyph));
        }
    }
}
