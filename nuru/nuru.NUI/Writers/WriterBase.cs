using System.IO;
using System.Text;

namespace nuru.NUI.Writers
{
    public abstract class WriterBase
    {
        protected BinaryWriter writer;

        public WriterBase(Stream stream)
        {
            writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
        }
    }
}
