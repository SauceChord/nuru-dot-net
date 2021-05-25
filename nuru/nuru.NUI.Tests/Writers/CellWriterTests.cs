using NUnit.Framework;
using nuru.NUI.Writers;

namespace nuru.NUI.Tests.Writers
{
    public class TestCellWriter : ReadWriteBaseTests, IGlyphWriter, IColorPairWriter, IMetadataWriter
    {
        protected string calls;
        protected CellWriter cellWriter;

        public override void Setup()
        {
            base.Setup();
            calls = "";
            cellWriter = new CellWriter(this, this, this);
        }

        [Test]
        public void ReadCellCallsInOrder()
        {
            cellWriter.Write(new Cell('A', new ColorPair(12, 2), 123));
            Assert.That(calls, Is.EqualTo("A12, 2123"));
        }

        void IGlyphWriter.Write(char glyph)
        {
            calls += glyph;
        }

        void IColorPairWriter.Write(ColorPair pair)
        {
            calls += pair;
        }

        void IMetadataWriter.Write(ushort metadata)
        {
            calls += metadata;
        }
    }
}
