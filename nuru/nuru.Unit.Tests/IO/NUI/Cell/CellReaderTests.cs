using BigEndian.IO;
using NUnit.Framework;
using nuru.IO.NUI.Cell;
using nuru.Unit.Tests;

namespace nuru.IO.NUI.Unit.Tests
{
    public class CellReaderTests : ReadWriteBaseTests, IGlyphReader, IColorReader, IMetadataReader
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

        NUIColor IColorReader.Read(BigEndianBinaryReader reader)
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
