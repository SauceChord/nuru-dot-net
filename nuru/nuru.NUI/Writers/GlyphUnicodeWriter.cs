using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public class GlyphUnicodeWriter : IGlyphWriter
    {
        public void Write(BigEndianBinaryWriter writer, char glyph)
        {
            writer.Write(glyph);
        }
    }
}
