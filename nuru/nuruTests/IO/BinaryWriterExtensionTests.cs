using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using nuru.IO;

namespace nuruTests.IO
{

    public class BinaryWriterExtensionTests
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
        public void WriteNURUASCIIWritesOneByte()
        {
            writer.WriteNURUASCII('A');
            stream.Position = 0;
            Assert.AreEqual(1, stream.Length);
            Assert.AreEqual(65, reader.ReadByte());
        }

        [Test]
        public void WriteNURUASCIIThrowsOnUnicodeOver255()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                    () => writer.WriteNURUASCII((char)256)
                );

            Assert.AreEqual(0, stream.Length);
            Assert.AreEqual("value", exception.ParamName);
        }

        [Test]
        public void WriteNURUUnicode()
        {
            // will be saved as big endian
            writer.WriteNURUUnicode((char)0x0100);
            stream.Position = 0;
            Assert.AreEqual(2, stream.Length);
            Assert.AreEqual(0x0001, reader.ReadUInt16());
        }

        [Test]
        public void WriteNURUStringFull()
        {
            // Saves ASCII only
            writer.WriteNURU("ABCDEFG");

            stream.Position = 0;
            var expected = new byte[] {
                65, 66, 67, 68, 69, 70, 71
            };
            Assert.AreEqual(7, stream.Length);
            Assert.AreEqual(expected, reader.ReadBytes(7));
        }

        [Test]
        public void WriteNURUStringPartial()
        {
            // Saves ASCII only
            writer.WriteNURU("ABCD");

            stream.Position = 0;
            var expected = new byte[] {
                65, 66, 67, 68, 0, 0, 0
            };
            Assert.AreEqual(7, stream.Length);
            Assert.AreEqual(expected, reader.ReadBytes(7));
        }

        [Test]
        public void WriteNURUStringEmpty()
        {
            // Saves ASCII only
            writer.WriteNURU("");

            stream.Position = 0;
            var expected = new byte[] {
                0, 0, 0, 0, 0, 0, 0
            };
            Assert.AreEqual(7, stream.Length);
            Assert.AreEqual(expected, reader.ReadBytes(7));
        }

        [Test]
        public void WriteNURUStringNULL()
        {
            // Saves ASCII only
            writer.WriteNURU(null);

            stream.Position = 0;
            var expected = new byte[] {
                0, 0, 0, 0, 0, 0, 0
            };
            Assert.AreEqual(7, stream.Length);
            Assert.AreEqual(expected, reader.ReadBytes(7));
        }

        [Test]
        public void WriteNURUStringThrowsOnUnicodeGreaterThan256()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                    // Saves ASCII only
                    // 'Ā' is 0x0100 which is illegal.
                    () => writer.WriteNURU("AĀĀĀĀĀZ")
                );
            Assert.AreEqual(0, stream.Length);
            Assert.AreEqual("value", exception.ParamName);
        }

        [Test]
        public void WriteNURUByte()
        {
            writer.WriteNURU((byte)0x12);
            stream.Position = 0;

            Assert.AreEqual(1, stream.Length);
            Assert.AreEqual(0x12, reader.ReadByte());
        }

        [Test]
        public void WriteNURUUInt16()
        {
            // Encodes into big endian
            writer.WriteNURU((ushort)0x1020);
            stream.Position = 0;

            Assert.AreEqual(2, stream.Length);
            Assert.AreEqual(0x2010, reader.ReadUInt16());
        }

        [Test]
        public void WriteNURUUInt32()
        {
            // Encodes into big endian
            writer.WriteNURU((uint)0x10203040);
            stream.Position = 0;

            Assert.AreEqual(4, stream.Length);
            Assert.AreEqual(0x40302010, reader.ReadUInt32());
        }
    }
}
