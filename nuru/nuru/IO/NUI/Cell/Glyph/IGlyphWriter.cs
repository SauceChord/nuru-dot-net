using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Glyph
{
    public interface IGlyphWriter
    {
        void Write(BigEndianBinaryWriter writer, char glyph);
    }
}
