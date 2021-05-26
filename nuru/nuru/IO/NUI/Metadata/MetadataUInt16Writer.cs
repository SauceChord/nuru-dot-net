using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class MetadataUInt16Writer : IMetadataWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, ushort metadata)
        {
            writer.WriteBigEndian(metadata);
        }
    }
}
