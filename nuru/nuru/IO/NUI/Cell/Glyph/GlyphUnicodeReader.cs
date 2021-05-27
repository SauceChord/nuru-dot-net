using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Glyph
{
    public class GlyphUnicodeReader : IGlyphReader
    {
        public virtual char Read(BigEndianBinaryReader reader)
        {
            return reader.ReadChar();
        }
    }
}
