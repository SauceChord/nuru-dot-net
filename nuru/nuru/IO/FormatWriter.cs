using BigEndian.IO;
using nuru.IO.NUI;
using nuru.IO.NUP;
using System.IO;
using System.Text;

namespace nuru.IO
{
    public class FormatWriter
    {
        public Stream BaseStream { get { return writer.BaseStream; } }

        protected BigEndianBinaryWriter writer;

        public FormatWriter(Stream stream)
        {
            writer = new BigEndianBinaryWriter(stream);
        }

        public void Write(string value, int length = 7)
        {
            var bytes = Encoding.ASCII.GetBytes(value.ToCharArray(), 0, length);
            if (bytes.Length < length)
            {
                var tmp = new byte[length];
                bytes.CopyTo(tmp, 0);
                bytes = tmp;
            }
            writer.Write(bytes);
        }

        public void Write(byte value)
        {
            writer.Write(value);
        }

        public void Write(ushort value)
        {
            writer.Write(value);
        }

        public void Write(GlyphMode value)
        {
            writer.Write((byte)value);
        }

        public void Write(ColorMode value)
        {
            writer.Write((byte)value);
        }

        public void Write(MetadataMode value)
        {
            writer.Write((byte)value);
        }

        public void Write(PaletteType value)
        {
            writer.Write((byte)value);
        }

        public void Write(byte[] bytes)
        {
            writer.Write(bytes);
        }
    }
}
