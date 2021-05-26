using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class MetadataVoidWriter : IMetadataWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, ushort metadata)
        {
        }
    }
}
