using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Metadata
{
    public class MetadataVoidWriter : IMetadataWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, ushort metadata)
        {
        }
    }
}
