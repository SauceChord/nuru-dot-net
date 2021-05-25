using NUnit.Framework;
using nuru.NUI.Readers;

namespace nuru.NUI.Tests.Readers
{
    public class TestCellReader : ReadWriteBaseTests, IGlyphReader, IColorPairReader, IMetadataReader
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
            Cell c = cellReader.Read();
            Assert.That(calls, Is.EqualTo("GCM"));
        }

        char IGlyphReader.Read()
        {
            calls += "G";
            return default;
        }

        ColorPair IColorPairReader.Read()
        {
            calls += "C";
            return default;
        }

        ushort IMetadataReader.Read()
        {
            calls += "M";
            return default;
        }
    }
}
