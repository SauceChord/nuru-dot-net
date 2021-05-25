using BigEndian.IO;
using NUnit.Framework;
using nuru.NUI.Readers;

namespace nuru.NUI.Tests.Readers
{
    public class CellReaderTests : ReadWriteBaseTests, IGlyphReader, IColorPairReader, IMetadataReader
    {
        protected string calls;
        protected CellReader cellReader;

        public override void Setup()
        {
            base.Setup();
            calls = "";
            cellReader = new CellReader(this, this, this);
        }

        [Test]
        public void ReadCellCallsInOrder()
        {
            cellReader.Read(null);
            Assert.That(calls, Is.EqualTo("GCM"));
        }

        char IGlyphReader.Read(BigEndianBinaryReader reader)
        {
            calls += "G";
            return default;
        }

        ColorPair IColorPairReader.Read(BigEndianBinaryReader reader)
        {
            calls += "C";
            return default;
        }

        ushort IMetadataReader.Read(BigEndianBinaryReader reader)
        {
            calls += "M";
            return default;
        }
    }
}
