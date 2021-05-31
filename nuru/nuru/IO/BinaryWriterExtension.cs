using System;
using System.IO;
using System.Text;
using System.Linq;

namespace nuru.IO
{
    public static class BinaryWriterExtensions
    {
        public static void WriteNURUASCII(this BinaryWriter writer, char value)
        {
            if (value > 0xff)
                throw new ArgumentOutOfRangeException("value");

            writer.Write((byte)value);
        }

        public static void WriteNURUUnicode(this BinaryWriter writer, char value)
        {
            writer.Write((byte)(value >> 8));
            writer.Write((byte)(value & 0xff));
        }

        public static void WriteNURU(this BinaryWriter writer, string value)
        {
            if (value == null)
            {
                writer.Write(new byte[7]);
                return;
            }

            if (value.Any(c => c >= 0x0100))
                throw new ArgumentOutOfRangeException("value");

            int length = Math.Min(7, value.Length);
            var bytes = new byte[7];
            var numBytes = Encoding.ASCII.GetBytes(value, 0, length, bytes, 0);
            writer.Write(bytes);
        }

        public static void WriteNURU(this BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }

        public static void WriteNURU(this BinaryWriter writer, ushort value)
        {
            writer.Write((byte)(value >> 8));
            writer.Write((byte)(value));
        }

        public static void WriteNURU(this BinaryWriter writer, uint value)
        {
            writer.Write((byte)(value >> 24));
            writer.Write((byte)(value >> 16));
            writer.Write((byte)(value >> 8));
            writer.Write((byte)(value));
        }
    }
}
