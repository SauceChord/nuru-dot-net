using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public class MetadataUInt16Writer : IMetadataWriter
    {
        public void Write(BigEndianBinaryWriter writer, ushort metadata)
        {
            writer.WriteBigEndian(metadata);
        }
    }
}
