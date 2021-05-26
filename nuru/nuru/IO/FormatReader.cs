using BigEndian.IO;
using System;
using System.IO;
using System.Text;
using System.Linq;

namespace nuru.IO
{
    public class FormatReader
    {
        public Stream BaseStream { get { return reader.BaseStream; } }

        protected BigEndianBinaryReader reader;

        public FormatReader(Stream stream)
        {
            reader = new BigEndianBinaryReader(stream);
        }

        public string ReadString(int length = 7)
        {
            var bytes = reader.ReadBytes(length);
            length = bytes.TakeWhile(b => b != 0).Count();
            return Encoding.ASCII.GetString(bytes, 0, length);
        }

        public byte ReadUInt8()
        {
            return reader.ReadByte();
        }

        public ushort ReadUInt16()
        {
            return reader.ReadBigEndianUInt16();
        }

        public ColorMode ReadColorMode()
        {
            return (ColorMode)reader.ReadByte();
        }

        public GlyphMode ReadGlyphMode()
        {
            return (GlyphMode)reader.ReadByte();
        }

        public MetadataMode ReadMetadataMode()
        {
            return (MetadataMode)reader.ReadByte();
        }

        public PaletteType ReadPaletteType()
        {
            return (PaletteType)reader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return reader.ReadBytes(count);
        }
    }
}
