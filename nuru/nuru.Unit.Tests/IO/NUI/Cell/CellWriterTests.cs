using BigEndian.IO;
using NUnit.Framework;
using nuru.Unit.Tests;

namespace nuru.IO.NUI.Unit.Tests
{
    public class CellWriterTests : ReadWriteBaseTests, IGlyphWriter, IColorWriter, IMetadataWriter
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
            cellWriter.Write(null, new Cell('A', new Color(12, 2), 123));
            Assert.That(calls, Is.EqualTo("A12, 2123"));
        }

        void IGlyphWriter.Write(BigEndianBinaryWriter writer, char glyph)
        {
            calls += glyph;
        }

        void IColorWriter.Write(BigEndianBinaryWriter writer, Color pair)
        {
            calls += pair;
        }

        void IMetadataWriter.Write(BigEndianBinaryWriter writer, ushort metadata)
        {
            calls += metadata;
        }
    }
}
