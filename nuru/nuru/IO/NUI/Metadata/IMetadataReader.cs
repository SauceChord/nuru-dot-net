using BigEndian.IO;

namespace nuru.IO.NUI
{
    public interface IMetadataReader
    {
        ushort Read(BigEndianBinaryReader reader);
    }
}
