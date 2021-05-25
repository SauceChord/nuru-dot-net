using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public interface IMetadataWriter
    {
        void Write(BigEndianBinaryWriter writer, ushort metadata);
    }
}
