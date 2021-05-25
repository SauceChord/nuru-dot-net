using System.IO;

namespace nuru.NUI.Writers
{
    public class MetadataUInt16Writer : WriterBase, IMetadataWriter
    {
        public MetadataUInt16Writer(Stream stream) : base(stream)
        {
        }

        public void Write(ushort metadata)
        {
            writer.WriteBigEndian(metadata);
        }
    }
}
