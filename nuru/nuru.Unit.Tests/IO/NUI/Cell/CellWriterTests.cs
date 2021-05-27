using BigEndian.IO;
using NUnit.Framework;
using nuru.IO.NUI.Cell;
using nuru.IO.NUI.Cell.Color;
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
            cellWriter.Write(null, new CellData('A', new ColorData(12, 2), 123));
            Assert.That(calls, Is.EqualTo("A12, 2123"));
        }

        void IGlyphWriter.Write(BigEndianBinaryWriter writer, char glyph)
        {
            calls += glyph;
        }

        void IColorWriter.Write(BigEndianBinaryWriter writer, ColorData pair)
        {
            calls += pair;
        }

        void IMetadataWriter.Write(BigEndianBinaryWriter writer, ushort metadata)
        {
            calls += metadata;
        }
    }
}
