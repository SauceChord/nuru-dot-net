using System;
using System.IO;

namespace nuru.NUI.Writers
{
    public class GlyphASCIIWriter : WriterBase, IGlyphWriter
    {
        public GlyphASCIIWriter(Stream stream) : base(stream)
        {
        }

        public void Write(char glyph)
        {
            writer.Write(Convert.ToByte(glyph));
        }
    }
}
