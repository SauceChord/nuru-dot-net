using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NUnit.Framework;

namespace nuru.IO.Unit.Tests
{
    public class FormatReaderTests
    {
        MemoryStream stream;
        BinaryWriter writer;
        FormatReader reader;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
            reader = new FormatReader(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
        }

        [Test]
        public void ReadString_GivenZeroByteArray_ShouldReturnEmptyString()
        {
            // Write 7 zeroes
            writer.Write(new byte[] { 0, 0, 0, 0, 0, 0, 0 });
            stream.Position = 0;
            
            string str = reader.ReadString();
            
            Assert.That(str, Is.EqualTo(string.Empty));
        }
    }
}
