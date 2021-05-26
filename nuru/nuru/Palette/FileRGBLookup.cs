using nuru.IO.NUP;
using System;

namespace nuru.Palette
{
    public class FileRGBLookup : IRGBLookup
    {
        readonly NUPFile nupFile;

        public FileRGBLookup(NUPFile nupFile)
        {
            this.nupFile = nupFile;
        }

        public RGB LookupRGB(byte index)
        {
            return nupFile.RGBPayload[index];
        }
    }
}
