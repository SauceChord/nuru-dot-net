using nuru.IO.NUP;
using System;

namespace nuru.Palette
{
    public class FileGlyphLookup : IGlyphLookup
    {
        readonly NUPFile nupFile;

        public FileGlyphLookup(NUPFile nupFile)
        {
            this.nupFile = nupFile;
        }

        public char LookupGlyph(char index)
        {
            return nupFile.UnicodePayload[(byte)index];
        }
    }
}
