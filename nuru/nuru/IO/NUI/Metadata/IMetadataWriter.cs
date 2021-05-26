using BigEndian.IO;

namespace nuru.IO.NUI
{
    public interface IMetadataWriter
    {
        void Write(BigEndianBinaryWriter writer, ushort metadata);
    }
}
