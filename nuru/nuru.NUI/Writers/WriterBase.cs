using BigEndian.IO;
using System.IO;
using System.Text;

namespace nuru.NUI.Writers
{
    public abstract class WriterBase
    {
        protected BigEndianBinaryWriter writer;

        public WriterBase(Stream stream)
        {
            writer = new BigEndianBinaryWriter(stream, Encoding.BigEndianUnicode, true);
        }
    }
}
