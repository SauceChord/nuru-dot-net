using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public interface IGlyphWriter
    {
        void Write(BigEndianBinaryWriter writer, char glyph);
    }
}
