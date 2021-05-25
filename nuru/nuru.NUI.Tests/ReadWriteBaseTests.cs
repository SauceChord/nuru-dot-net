using BigEndian.IO;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace nuru.NUI.Tests
{
    public class ReadWriteBaseTests
    {
        protected MemoryStream stream;
        protected BigEndianBinaryReader reader;
        protected BigEndianBinaryWriter writer;

        [SetUp]
        public virtual void Setup()
        {
            stream = new MemoryStream();
            reader = new BigEndianBinaryReader(stream, Encoding.BigEndianUnicode, true);
            writer = new BigEndianBinaryWriter(stream, Encoding.BigEndianUnicode, true);
        }

        public virtual void RewindStream()
        {
            stream.Position = 0;
        }

        [TearDown]
        public virtual void Teardown()
        {
            stream.Dispose();
        }
    }
}
