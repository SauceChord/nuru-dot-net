using NUnit.Framework;
using nuru.IO;
using System;
using System.IO;

namespace nuruTests.IO
{
    public class ASCIICollectionTests
    {
        [Test]
        public void ConstructorSetsDimensions()
        {
            var collection = new ASCIICollection(2, 3);
            Assert.AreEqual(2, collection.Width);
            Assert.AreEqual(3, collection.Height);
        }

        [Test]
        public void CanGetSetValues()
        {
            var collection = new ASCIICollection(2, 3);
            collection.Set(0, 0, 'A');
            collection.Set(1, 0, 'B');
            collection.Set(0, 1, 'C');
            collection.Set(1, 1, 'D');
            collection.Set(0, 2, 'E');
            collection.Set(1, 2, 'F');

            Assert.AreEqual('A', collection.Get(0, 0));
            Assert.AreEqual('B', collection.Get(1, 0));
            Assert.AreEqual('C', collection.Get(0, 1));
            Assert.AreEqual('D', collection.Get(1, 1));
            Assert.AreEqual('E', collection.Get(0, 2));
            Assert.AreEqual('F', collection.Get(1, 2));
        }

        [Test]
        public void SetThrowsOnUnicodeGreaterThan255()
        {
            var collection = new ASCIICollection(1, 1);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                    () => collection.Set(0, 0, (char)256)
                );
            Assert.AreEqual("value", exception.ParamName);
        }


        [Test]
        public void LoadThrowsXArgumentOutOfRangeException()
        {
            var collection = new ASCIICollection(1, 1);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                    () => collection.Load(1, 0, null)
                );
            Assert.AreEqual("x", exception.ParamName);
        }

        [Test]
        public void LoadThrowsYArgumentOutOfRangeException()
        {
            var collection = new ASCIICollection(1, 1);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                    () => collection.Load(0, 1, null)
                );
            Assert.AreEqual("y", exception.ParamName);
        }

        [Test]
        public void Load()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.WriteNURUASCII('A');
            stream.Position = 0;

            var reader = new BinaryReader(stream);
            var collection = new ASCIICollection(2, 2);
            collection.Set(1, 1, 'Z');
            collection.Load(1, 1, reader);
            Assert.AreEqual('A', collection.Get(1, 1));
        }

        [Test]
        public void Save()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            var collection = new ASCIICollection(2, 2);
            collection.Set(1, 1, 'A');
            collection.Save(1, 1, writer);

            stream.Position = 0;
            var reader = new BinaryReader(stream);
            Assert.AreEqual((byte)'A', reader.ReadByte());
        }
    }
}
