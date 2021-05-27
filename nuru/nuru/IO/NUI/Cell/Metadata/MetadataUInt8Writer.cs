using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Metadata
{
    public class MetadataUInt8Writer : IMetadataWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, ushort metadata)
        {
            writer.Write((byte)metadata);
        }
    }
}
