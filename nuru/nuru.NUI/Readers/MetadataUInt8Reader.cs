using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class MetadataUInt8Reader : IMetadataReader
    {
        public ushort Read(BigEndianBinaryReader reader)
        {
            return reader.ReadByte();
        }
    }
}
