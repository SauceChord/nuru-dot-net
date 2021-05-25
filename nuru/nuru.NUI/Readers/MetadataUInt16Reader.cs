using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class MetadataUInt16Reader : IMetadataReader
    {
        public ushort Read(BigEndianBinaryReader reader)
        {
            return reader.ReadBigEndianUInt16();
        }
    }
}
