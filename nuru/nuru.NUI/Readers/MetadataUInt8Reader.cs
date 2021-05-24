using System.IO;

namespace nuru.NUI.Readers
{
    public class MetadataUInt8Reader : ReaderBase, IMetadataReader
    {
        public MetadataUInt8Reader(Stream stream) : base(stream)
        {
        }

        public ushort Read()
        {
            return reader.ReadByte();
        }
    }
}
