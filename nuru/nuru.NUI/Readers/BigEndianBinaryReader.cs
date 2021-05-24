using System;
using System.IO;
using System.Text;

namespace nuru.NUI.Readers
{
    public class BigEndianBinaryReader : BinaryReader
    {
        public BigEndianBinaryReader(Stream input) : base(input)
        {
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }

        public override float ReadSingle()
        {
            // Simply because I haven't had time to look into how endianness works here.
            throw new NotImplementedException();
        }

        public override double ReadDouble()
        {
            // Simply because I haven't had time to look into how endianness works here.
            throw new NotImplementedException();
        }

        public override decimal ReadDecimal()
        {
            // Simply because I haven't had time to look into how endianness works here.
            throw new NotImplementedException();
        }

        public override short ReadInt16()
        {
            var data = base.ReadBytes(sizeof(short));
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        public override int ReadInt32()
        {
            var data = base.ReadBytes(sizeof(int));
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        public override long ReadInt64()
        {
            var data = base.ReadBytes(sizeof(long));
            Array.Reverse(data);
            return BitConverter.ToInt64(data, 0);
        }

        public override ushort ReadUInt16()
        {
            var data = base.ReadBytes(sizeof(ushort));
            Array.Reverse(data);
            return BitConverter.ToUInt16(data, 0);
        }

        public override uint ReadUInt32()
        {
            var data = base.ReadBytes(sizeof(uint));
            Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }

        public override ulong ReadUInt64()
        {
            var data = base.ReadBytes(sizeof(ulong));
            Array.Reverse(data);
            return BitConverter.ToUInt64(data, 0);
        }
    }
}
