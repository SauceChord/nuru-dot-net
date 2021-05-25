using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class GlyphSpaceReader : IGlyphReader
    {
        public char Read(BigEndianBinaryReader reader)
        {
            return ' ';
        }
    }
}
