using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Metadata
{
    public class MetadataVoidReader : IMetadataReader
    {
        public virtual ushort Read(BigEndianBinaryReader reader)
        {
            return default;
        }
    }
}
