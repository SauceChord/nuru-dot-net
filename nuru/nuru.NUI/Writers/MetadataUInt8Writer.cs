using System.IO;

namespace nuru.NUI.Writers
{
    public class MetadataUInt8Writer : WriterBase, IMetadataWriter
    {
        public MetadataUInt8Writer(Stream stream) : base(stream)
        {
        }

        public void Write(ushort metadata)
        {
            writer.Write((byte)metadata);
        }
    }
}
