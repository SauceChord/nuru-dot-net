using BigEndian.IO;

namespace nuru.IO.NUI
{
    public interface IGlyphWriter
    {
        void Write(BigEndianBinaryWriter writer, char glyph);
    }
}
