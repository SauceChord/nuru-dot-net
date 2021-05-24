using System.IO;

namespace nuru.NUI.Readers
{
    public class GlyphUnicodeReader : ReaderBase, IGlyphReader
    {
        public GlyphUnicodeReader(Stream stream) : base(stream)
        {
        }

        public char Read()
        {
            return reader.ReadChar();
        }
    }
}
