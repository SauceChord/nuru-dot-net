using System;
using System.Text;

namespace nuru.IO
{
    public enum PaletteType : byte
    {
        ANSI8 = 1,
        Unicode = 2,
        RGB = 3,
    }

    public class PaletteFile
    {
        public const string Signature = "NURUPAL";
        public const byte Version = 1;
        public readonly PaletteType PaletteType;
        public char KeyChar = ' ';
        public byte KeyFore = 15;
        public byte KeyBack = 0;
        public uint Userdata;

        private const int entryCount = 256;
        private byte[] entries;

        public PaletteFile(PaletteType paletteType)
        {
            switch (paletteType)
            {
                case PaletteType.ANSI8:
                case PaletteType.Unicode:
                case PaletteType.RGB:
                    PaletteType = paletteType;
                    entries = new byte[entryCount * (int)paletteType];
                    break;
                default:
                    throw new ArgumentOutOfRangeException("paletteType");
            }
        }

        public void SetANSI8(byte index, byte ansi8)
        {
            if (PaletteType != PaletteType.ANSI8)
                throw new InvalidOperationException($"Invalid call for PaletteType {PaletteType}");

            entries[index] = ansi8;
        }

        public byte GetANSI8(byte index)
        {
            if (PaletteType != PaletteType.ANSI8)
                throw new InvalidOperationException($"Invalid call for PaletteType {PaletteType}");

            return entries[index];
        }

        public void SetChar(byte index, char value)
        {
            if (PaletteType != PaletteType.Unicode)
                throw new InvalidOperationException($"Invalid call for PaletteType {PaletteType}");

            var idx = index * 2;
            entries[idx] = (byte)(value & 0xff);
            entries[idx + 1] = (byte)(value >> 8);
        }

        public char GetChar(byte index)
        {
            if (PaletteType != PaletteType.Unicode)
                throw new InvalidOperationException($"Invalid call for PaletteType {PaletteType}");

            var idx = index * 2;
            return (char)(entries[idx] | entries[idx + 1] << 8);
        }

        public void SetRGB(byte index, RGB rgb)
        {
            if (PaletteType != PaletteType.RGB)
                throw new InvalidOperationException($"Invalid call for PaletteType {PaletteType}");

            int idx = index * 3;
            entries[idx + 0] = rgb.Red;
            entries[idx + 1] = rgb.Green;
            entries[idx + 2] = rgb.Blue;
        }

        public RGB GetRGB(byte index)
        {
            if (PaletteType != PaletteType.RGB)
                throw new InvalidOperationException($"Invalid call for PaletteType {PaletteType}");

            int idx = index * 3;
            return new RGB(
                entries[idx + 0],
                entries[idx + 1],
                entries[idx + 2]);
        }
    }
}
