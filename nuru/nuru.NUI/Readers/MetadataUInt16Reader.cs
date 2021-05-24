using System.IO;

namespace nuru.NUI.Readers
{
    public class MetadataUInt16Reader : ReaderBase, IMetadataReader
    {
        public MetadataUInt16Reader(Stream stream) : base(stream)
        { 
        }

        public ushort Read()
        {
            return reader.ReadUInt16();
        }
    }
}
