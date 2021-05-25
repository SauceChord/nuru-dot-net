using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public interface IMetadataReader
    {
        ushort Read(BigEndianBinaryReader reader);
    }
}
