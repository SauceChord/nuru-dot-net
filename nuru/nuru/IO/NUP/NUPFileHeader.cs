using System.IO;

namespace nuru.IO.NUP
{
    public class NUPFileHeader
    {
        public const string Signature = "NURUPAL";

        public byte Version;
        public PaletteType Type;
        public byte KeyGlyphIndex;
        public byte KeyForegroundIndex;
        public byte KeyBackgroundIndex;
        public byte[] Userdata;

        public static NUPFileHeader FromStream(Stream stream)
        {
            var format = new FormatReader(stream);
            var signature = format.ReadString();

            if (signature != Signature)
                throw new FormatException("Bad header signature");

            return new NUPFileHeader()
            {
                Version = format.ReadUInt8(),
                Type = format.ReadPaletteType(),
                KeyGlyphIndex = format.ReadUInt8(),
                KeyForegroundIndex = format.ReadUInt8(),
                KeyBackgroundIndex = format.ReadUInt8(),
                Userdata = format.ReadBytes(4),
            };
        }

        public void ToStream(Stream stream)
        {
            var format = new FormatWriter(stream);
            format.Write(Signature);
            format.Write(Version);
            format.Write(Type);
            format.Write(KeyGlyphIndex);
            format.Write(KeyForegroundIndex);
            format.Write(KeyBackgroundIndex);
            format.Write(Userdata);
        }
    }
}
