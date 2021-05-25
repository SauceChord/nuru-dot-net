using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class MetadataVoidReader : IMetadataReader
    {
        public ushort Read(BigEndianBinaryReader reader)
        {
            return default;
        }
    }
}
