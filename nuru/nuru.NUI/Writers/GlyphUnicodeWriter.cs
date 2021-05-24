using System.IO;

namespace nuru.NUI.Writers
{
    public class GlyphUnicodeWriter : WriterBase, IGlyphWriter
    {
        public GlyphUnicodeWriter(Stream stream) : base(stream)
        {
        }

        public void Write(char glyph)
        {
            writer.Write(glyph);
        }
    }
}
