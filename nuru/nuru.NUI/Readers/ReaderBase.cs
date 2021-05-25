using BigEndian.IO;
using System.IO;
using System.Text;

namespace nuru.NUI.Readers
{
    public abstract class ReaderBase
    {
        protected BigEndianBinaryReader reader;

        public ReaderBase(Stream stream)
        {
            reader = new BigEndianBinaryReader(stream, Encoding.BigEndianUnicode, true);
        }
    }
}
