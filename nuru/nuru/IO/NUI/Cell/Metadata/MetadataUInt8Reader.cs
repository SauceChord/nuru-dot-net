using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Metadata
{
    public class MetadataUInt8Reader : IMetadataReader
    {
        public virtual ushort Read(BigEndianBinaryReader reader)
        {
            return reader.ReadByte();
        }
    }
}
