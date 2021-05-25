using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public interface IGlyphReader
    {
        char Read(BigEndianBinaryReader reader);
    }
}
