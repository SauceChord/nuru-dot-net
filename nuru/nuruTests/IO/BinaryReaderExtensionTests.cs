using NUnit.Framework;
using nuru.IO;
using System.IO;

namespace nuruTests.IO
{
    public class BinaryReaderExtensionTests
    {
        MemoryStream stream;
        BinaryReader reader;
        BinaryWriter writer;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
        }

        [Test]
        public void ReadNURUASCIIReadsOneByte()
        {
            writer.Write((byte)65);
            writer.Write((byte)66);
            stream.Position = 0;
            Assert.AreEqual('A', reader.ReadNURUASCII());
            Assert.AreEqual('B', reader.ReadNURUASCII());
            Assert.AreEqual(2, stream.Position);
        }

        [Test]
        public void ReadNURUUnicodeReadsBigEndian()
        {
            // write 0001 big endian, which is 0100 little endian
            writer.Write((ushort)0x0001);
            stream.Position = 0;
            Assert.AreEqual('Ā', reader.ReadNURUUnicode());
            Assert.AreEqual(2, stream.Position);
        }

        [Test]
        public void ReadNURUStringFull()
        {
            // A nuru string by def is 7 ascii chars long at most
            writer.Write((byte)'A');
            writer.Write((byte)'B');
            writer.Write((byte)'C');
            writer.Write((byte)'D');
            writer.Write((byte)'E');
            writer.Write((byte)'F');
            writer.Write((byte)'G');
            stream.Position = 0;
            Assert.AreEqual("ABCDEFG", reader.ReadNURUString());
            Assert.AreEqual(7, stream.Position);
        }

        [Test]
        public void ReadNURUStringPartial()
        {
            // A nuru string is zero terminated
            // Always reads 7 bytes though.
            writer.Write((byte)'A');
            writer.Write((byte)'B');
            writer.Write((byte)'C');
            writer.Write((byte)0); // zero termination
            writer.Write((byte)'E');
            writer.Write((byte)'F');
            writer.Write((byte)'G');
            stream.Position = 0;
            Assert.AreEqual("ABC", reader.ReadNURUString());
            Assert.AreEqual(7, stream.Position);
        }

        [Test]
        public void ReadNURUStringEmpty()
        {
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            stream.Position = 0;
            Assert.AreEqual("", reader.ReadNURUString());
            Assert.AreEqual(7, stream.Position);
        }

        [Test]
        public void ReadNURUUInt16()
        {
            // Write 0x1020 in big endian
            writer.Write((ushort)0x2010);
            stream.Position = 0;
            Assert.AreEqual(0x1020, reader.ReadNURUUInt16());
            Assert.AreEqual(2, stream.Position);
        }

        [Test]
        public void ReadNURUUInt32()
        {
            // Write 0x10203040 in big endian
            writer.Write((uint)0x40302010);
            stream.Position = 0;
            Assert.AreEqual(0x10203040, reader.ReadNURUUInt32());
            Assert.AreEqual(4, stream.Position);
        }
    }
}
