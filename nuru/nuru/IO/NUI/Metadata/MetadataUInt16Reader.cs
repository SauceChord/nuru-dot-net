using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class MetadataUInt16Reader : IMetadataReader
    {
        public virtual ushort Read(BigEndianBinaryReader reader)
        {
            return reader.ReadBigEndianUInt16();
        }
    }
}
