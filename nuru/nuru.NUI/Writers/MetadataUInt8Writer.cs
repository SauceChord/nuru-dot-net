using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public class MetadataUInt8Writer : IMetadataWriter
    {
        public void Write(BigEndianBinaryWriter writer, ushort metadata)
        {
            writer.Write((byte)metadata);
        }
    }
}
