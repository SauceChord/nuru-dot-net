using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class MetadataUInt8Reader : IMetadataReader
    {
        public virtual ushort Read(BigEndianBinaryReader reader)
        {
            return reader.ReadByte();
        }
    }
}
