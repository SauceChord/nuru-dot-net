using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class GlyphUnicodeWriter : IGlyphWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, char glyph)
        {
            writer.Write(glyph);
        }
    }
}
