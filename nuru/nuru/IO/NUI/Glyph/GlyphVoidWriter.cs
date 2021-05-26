using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class GlyphVoidWriter : IGlyphWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, char glyph)
        {
        }
    }
}
