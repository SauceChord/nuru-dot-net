using NUnit.Framework;
using System.IO;
using System.Text;

namespace nuru.NUI.Tests
{
    public class ReadWriteBaseTests
    {
        protected MemoryStream stream;
        protected BinaryReader reader;
        protected BinaryWriter writer;

        [SetUp]
        public virtual void Setup()
        {
            stream = new MemoryStream();
            reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);
            writer = new BinaryWriter(stream, Encoding.BigEndianUnicode, true);
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
