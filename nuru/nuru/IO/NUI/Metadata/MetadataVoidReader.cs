using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class MetadataVoidReader : IMetadataReader
    {
        public virtual ushort Read(BigEndianBinaryReader reader)
        {
            return default;
        }
    }
}
