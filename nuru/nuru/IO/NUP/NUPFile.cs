using System.IO;
using System.Text;

namespace nuru.IO.NUP
{
    public class NUPFile
    {
        public const int PayloadCount = 256;
        public NUPFileHeader Header;
        public byte[] ANSIPayload;
        public char[] UnicodePayload;
        public RGB[] RGBPayload;

        public NUPFile(NUPFileHeader header, byte[] ansiPayload, char[] unicodePayload, RGB[] rgbPayload)
        {
            Header = header;
            ANSIPayload = ansiPayload;
            UnicodePayload = unicodePayload;
            RGBPayload = rgbPayload;
        }

        public static NUPFile FromStream(Stream stream)
        {
            var header = NUPFileHeader.FromStream(stream);
            byte[] ansiPayload = null;
            char[] unicodePayload = null;
            RGB[] rgbPayload = null;

            var reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);

            switch (header.Type)
            {
                case PaletteType.Ansi:
                    ansiPayload = reader.ReadBytes(PayloadCount);
                    break;
                case PaletteType.Unicode:
                    unicodePayload = reader.ReadChars(PayloadCount);
                    break;
                case PaletteType.RGB:
                    rgbPayload = new RGB[PayloadCount];
                    for (int i = 0; i < PayloadCount; ++i)
                    {
                        var r = reader.ReadByte();
                        var g = reader.ReadByte();
                        var b = reader.ReadByte();
                        rgbPayload[i] = new RGB(r, g, b);
                    }
                    break;
                default:
                    throw new FormatException("Bad palette type");
            }

            return new NUPFile(header, ansiPayload, unicodePayload, rgbPayload);
        }

        public void ToStream(Stream stream)
        {
            Header.ToStream(stream);

            var writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);

            switch (Header.Type)
            {
                case PaletteType.Ansi:
                    writer.Write(ANSIPayload);
                    break;
                case PaletteType.Unicode:
                    writer.Write(UnicodePayload);
                    break;
                case PaletteType.RGB:
                    for (int i = 0; i < PayloadCount; ++i)
                    {
                        writer.Write(RGBPayload[i].R);
                        writer.Write(RGBPayload[i].G);
                        writer.Write(RGBPayload[i].B);
                    }
                    break;
                default:
                    throw new FormatException("Bad palette type");
            }
        }

        public static NUPFile FromFile(string path)
        {
            using (FileStream stream = File.OpenRead(path))
                return FromStream(stream);
        }

        public void ToFile(string path)
        {
            using (FileStream stream = File.OpenWrite(path))
                ToStream(stream);
        }
    }
}
