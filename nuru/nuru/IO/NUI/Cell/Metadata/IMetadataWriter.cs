using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Metadata
{
    public interface IMetadataWriter
    {
        void Write(BigEndianBinaryWriter writer, ushort metadata);
    }
}
