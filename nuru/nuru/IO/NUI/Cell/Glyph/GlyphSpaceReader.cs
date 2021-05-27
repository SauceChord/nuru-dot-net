using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Glyph
{
    public class GlyphSpaceReader : IGlyphReader
    {
        public virtual char Read(BigEndianBinaryReader reader)
        {
            return ' ';
        }
    }
}
