using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class GlyphSpaceReader : IGlyphReader
    {
        public virtual char Read(BigEndianBinaryReader reader)
        {
            return ' ';
        }
    }
}
