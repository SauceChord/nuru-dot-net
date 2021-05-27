using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Glyph
{
    public class GlyphVoidWriter : IGlyphWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, char glyph)
        {
        }
    }
}
