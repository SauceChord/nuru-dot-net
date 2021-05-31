using System;
using System.IO;

namespace nuru.IO
{
    public class ASCIICollection
    {
        protected readonly char[,] arr;
        public ASCIICollection(ushort width, ushort height)
        {
            arr = new char[width, height];
        }

        public ushort Width { get { return (ushort)arr.GetLength(0); } }
        public ushort Height { get { return (ushort)arr.GetLength(1); } }

        public void Set(ushort x, ushort y, char value)
        {
            if (value > 0xff)
                throw new ArgumentOutOfRangeException("value");

            arr[x, y] = value;
        }

        public char Get(ushort x, ushort y)
        {
            return arr[x, y];
        }

        public void Load(ushort x, ushort y, BinaryReader reader)
        {
            if (x >= Width)
                throw new ArgumentOutOfRangeException("x");

            if (y >= Height)
                throw new ArgumentOutOfRangeException("y");

            arr[x, y] = reader.ReadNURUASCII();
        }

        public void Save(ushort x, ushort y, BinaryWriter writer)
        {
            writer.WriteNURUASCII(arr[x, y]);
        }
    }
}
