using NUnit.Framework;
using nuru.NUI.Readers;
using System;

namespace nuru.NUI.Tests.Readers
{
    public class TestBigEndianBinaryReader : TestReadWriteBase
    {
        protected BigEndianBinaryReader bigEndianReader;

        public override void Setup()
        {
            base.Setup();
            bigEndianReader = new BigEndianBinaryReader(stream);
        }

        [Test]
        public void TestReadSingleThrowsNotImplementedException()
        {
            // Simply because I haven't had time to look into how endianness works here.
            Assert.Throws<NotImplementedException>(() => bigEndianReader.ReadSingle());
        }

        [Test]
        public void TestReadDoubleThrowsNotImplementedException()
        {
            // Simply because I haven't had time to look into how endianness works here.
            Assert.Throws<NotImplementedException>(() => bigEndianReader.ReadDouble());
        }

        [Test]
        public void TestReadDecimalThrowsNotImplementedException()
        {
            // Simply because I haven't had time to look into how endianness works here.
            Assert.Throws<NotImplementedException>(() => bigEndianReader.ReadDecimal());
        }

        [TestCase((short)0x0000, (short)0x0000)]
        [TestCase((short)0x0100, (short)0x0001)]
        [TestCase((short)0x0001, (short)0x0100)]
        [TestCase(unchecked((short)0xff01), (short)0x01ff)]
        public void TestReadBigEndianInt16(short bigEndian, short littleEndian)
        {
            writer.Write(bigEndian);
            RewindStream();

            Assert.That(bigEndianReader.ReadInt16(), Is.EqualTo(littleEndian));
            Assert.That(bigEndianReader.BaseStream.Position, Is.EqualTo(2));
        }

        [TestCase((ushort)0x0000, (ushort)0x0000)]
        [TestCase((ushort)0x0100, (ushort)0x0001)]
        [TestCase((ushort)0x0001, (ushort)0x0100)]
        [TestCase((ushort)0xff01, (ushort)0x01ff)]
        public void TestReadBigEndianUInt16(ushort bigEndian, ushort littleEndian)
        {
            writer.Write(bigEndian);
            RewindStream();

            Assert.That(bigEndianReader.ReadUInt16(), Is.EqualTo(littleEndian));
            Assert.That(bigEndianReader.BaseStream.Position, Is.EqualTo(2));
        }

        [TestCase((int)0x00000000, (int)0x00000000)]
        [TestCase((int)0x01000000, (int)0x00000001)]
        [TestCase((int)0x00010000, (int)0x00000100)]
        [TestCase((int)0x00000100, (int)0x00010000)]
        [TestCase((int)0x00000001, (int)0x01000000)]
        [TestCase(unchecked((int)0xffffffff), (int)-0x00000001)]
        [TestCase(unchecked((int)0xfeffffff), (int)-0x00000002)]
        public void TestReadBigEndianInt32(int bigEndian, int littleEndian)
        {
            writer.Write(bigEndian);
            RewindStream();

            Assert.That(bigEndianReader.ReadInt32(), Is.EqualTo(littleEndian));
            Assert.That(bigEndianReader.BaseStream.Position, Is.EqualTo(4));
        }

        [TestCase((uint)0x00000000, (uint)0x00000000)]
        [TestCase((uint)0x01000000, (uint)0x00000001)]
        [TestCase((uint)0x00010000, (uint)0x00000100)]
        [TestCase((uint)0x00000100, (uint)0x00010000)]
        [TestCase((uint)0x00000001, (uint)0x01000000)]
        public void TestReadBigEndianUInt32(uint bigEndian, uint littleEndian)
        {
            writer.Write(bigEndian);
            RewindStream();

            Assert.That(bigEndianReader.ReadUInt32(), Is.EqualTo(littleEndian));
            Assert.That(bigEndianReader.BaseStream.Position, Is.EqualTo(4));
        }

        [TestCase((long)0x0000000000000000, (long)0x0000000000000000)]
        [TestCase((long)0x0100000000000000, (long)0x0000000000000001)]
        [TestCase((long)0x0001000000000000, (long)0x0000000000000100)]
        [TestCase((long)0x0000010000000000, (long)0x0000000000010000)]
        [TestCase((long)0x0000000100000000, (long)0x0000000001000000)]
        [TestCase((long)0x0000000001000000, (long)0x0000000100000000)]
        [TestCase((long)0x0000000000010000, (long)0x0000010000000000)]
        [TestCase((long)0x0000000000000100, (long)0x0001000000000000)]
        [TestCase((long)0x0000000000000001, (long)0x0100000000000000)]
        [TestCase(unchecked((long)0xffffffffffffffff), (long)-0x0000000000000001)]
        [TestCase(unchecked((long)0xfeffffffffffffff), (long)-0x0000000000000002)]
        public void TestReadBigEndianInt32(long bigEndian, long littleEndian)
        {
            writer.Write(bigEndian);
            RewindStream();

            Assert.That(bigEndianReader.ReadInt64(), Is.EqualTo(littleEndian));
            Assert.That(bigEndianReader.BaseStream.Position, Is.EqualTo(8));
        }

        [TestCase((ulong)0x0000000000000000, (ulong)0x0000000000000000)]
        [TestCase((ulong)0x0100000000000000, (ulong)0x0000000000000001)]
        [TestCase((ulong)0x0001000000000000, (ulong)0x0000000000000100)]
        [TestCase((ulong)0x0000010000000000, (ulong)0x0000000000010000)]
        [TestCase((ulong)0x0000000100000000, (ulong)0x0000000001000000)]
        [TestCase((ulong)0x0000000001000000, (ulong)0x0000000100000000)]
        [TestCase((ulong)0x0000000000010000, (ulong)0x0000010000000000)]
        [TestCase((ulong)0x0000000000000100, (ulong)0x0001000000000000)]
        [TestCase((ulong)0x0000000000000001, (ulong)0x0100000000000000)]
        public void TestReadBigEndianUInt32(ulong bigEndian, ulong littleEndian)
        {
            writer.Write(bigEndian);
            RewindStream();

            Assert.That(bigEndianReader.ReadUInt64(), Is.EqualTo(littleEndian));
            Assert.That(bigEndianReader.BaseStream.Position, Is.EqualTo(8));
        }
    }
}
