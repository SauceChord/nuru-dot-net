using BigEndian.IO;

namespace nuru.IO.NUI
{
    public interface IGlyphReader
    {
        char Read(BigEndianBinaryReader reader);
    }
}
