using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Glyph
{
    public interface IGlyphReader
    {
        char Read(BigEndianBinaryReader reader);
    }
}
