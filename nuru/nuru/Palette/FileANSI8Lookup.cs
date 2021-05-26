using nuru.IO.NUP;
using System;

namespace nuru.Palette
{
    public class FileANSI8Lookup : IRGBLookup
    {
        readonly NUPFile nupFile;

        public FileANSI8Lookup(NUPFile nupFile)
        {
            this.nupFile = nupFile;
        }

        public RGB LookupRGB(byte index)
        {
            byte ansi = nupFile.ANSIPayload[index];
            return new RGB(ConstANSI8Lookup.rawRGB[ansi]);
        }
    }
}
