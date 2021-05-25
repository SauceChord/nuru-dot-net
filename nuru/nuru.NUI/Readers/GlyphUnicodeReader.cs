using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class GlyphUnicodeReader : IGlyphReader
    {
        public char Read(BigEndianBinaryReader reader)
        {
            return reader.ReadChar();
        }
    }
}
