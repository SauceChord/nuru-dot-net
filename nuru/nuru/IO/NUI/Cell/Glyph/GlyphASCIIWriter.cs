using BigEndian.IO;
using System;

namespace nuru.IO.NUI.Cell.Glyph
{
    public class GlyphASCIIWriter : IGlyphWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, char glyph)
        {
            writer.Write(Convert.ToByte(glyph));
        }
    }
}
