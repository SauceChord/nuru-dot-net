using System.IO;
using System.Linq;
using System.Text;

namespace nuru.IO
{
    public static class BinaryReaderExtension
    {
        public static char ReadNURUASCII(this BinaryReader reader)
        {
            return (char)reader.ReadByte();
        }

        public static char ReadNURUUnicode(this BinaryReader reader)
        {
            // Format is big endian
            byte first = reader.ReadByte();
            byte second = reader.ReadByte();
            return (char)(first << 8 | second);
        }

        public static string ReadNURUString(this BinaryReader reader)
        {
            var bytes = reader.ReadBytes(7);
            var length = bytes.TakeWhile(b => b != 0).Count();
            return Encoding.ASCII.GetString(bytes, 0, length);
        }

        public static byte ReadNURUUInt8(this BinaryReader reader)
        {
            return reader.ReadByte();
        }

        public static ushort ReadNURUUInt16(this BinaryReader reader)
        {
            // Format is big endian
            byte first = reader.ReadByte();
            byte second = reader.ReadByte();
            return (ushort)(first << 8 | second);
        }

        public static uint ReadNURUUInt32(this BinaryReader reader)
        {
            // Format is big endian
            byte first = reader.ReadByte();
            byte second = reader.ReadByte();
            byte third = reader.ReadByte();
            byte fourth = reader.ReadByte();
            return (uint)(first << 24 | second << 16 | third << 8 | fourth);
        }
    }
}
