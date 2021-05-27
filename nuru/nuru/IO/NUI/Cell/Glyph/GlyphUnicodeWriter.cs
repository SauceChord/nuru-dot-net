using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Glyph
{
    public class GlyphUnicodeWriter : IGlyphWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, char glyph)
        {
            writer.Write(glyph);
        }
    }
}
