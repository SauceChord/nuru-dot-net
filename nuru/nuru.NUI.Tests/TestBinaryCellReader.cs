using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestBinaryCellReader
    {
        MemoryStream stream;
        BinaryGlyphWriter glyphWriter;
        BinaryColorPairWriter colorWriter;
        //BinaryMetadataWriter metadataWriter;

        [SetUp]
        public void Setup()
        {
            stream = new MemoryStream();
            glyphWriter = new BinaryGlyphWriter(stream);
            colorWriter = new BinaryColorPairWriter(stream);
        }

        [TearDown]
        public void Teardown()
        {
            stream.Dispose();
        }

        [Test]
        public void TestReadCell()
        {
            Cell expectedCell = new Cell('A', 12, 2, 0);
            glyphWriter.WriteUTF16(expectedCell.Character);
            colorWriter.Write4BitsPerChannel(expectedCell.Colors);
            stream.Position = 0;
            
            var reader = new BinaryCellReader(stream, new CellMode(GlyphMode.UTF16, ColorMode.FourBit, MetadataMode.None));
            Cell actualCell = reader.ReadCell();

            Assert.That(actualCell, Is.EqualTo(expectedCell));
        }
    }
}
