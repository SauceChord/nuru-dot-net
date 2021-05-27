using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Metadata
{
    public interface IMetadataReader
    {
        ushort Read(BigEndianBinaryReader reader);
    }
}
