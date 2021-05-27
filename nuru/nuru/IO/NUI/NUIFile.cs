using System.IO;
using BigEndian.IO;
using nuru.IO.NUI.Cell;
using nuru.IO.NUI.Cell.Color;
using nuru.IO.NUI.Cell.Glyph;

namespace nuru.IO.NUI
{
    public class NUIFile
    {
        public readonly static CellReaderFactory CellReaderFactory;
        public readonly static CellWriterFactory CellWriterFactory;

        public NUIFileHeader Header;
        public Cell.CellData[,] Payload;

        public CellConfig CellConfig
        {
            get { return Header.GetCellConfig(); }
        }

        public string GlyphFileName { get { return Header.GlyphMode == GlyphMode.Palette ? Header.GlyphPaletteName : null; } }
        public string ColorFileName { get { return Header.ColorMode == ColorMode.Palette ? Header.ColorPaletteName : null; } }

        static NUIFile()
        {
            CellReaderFactory = new CellReaderFactory();

            CellReaderFactory.RegisterGlyphReader(GlyphMode.None, new GlyphSpaceReader());
            CellReaderFactory.RegisterGlyphReader(GlyphMode.ASCII, new GlyphASCIIReader());
            CellReaderFactory.RegisterGlyphReader(GlyphMode.UTF16, new GlyphUnicodeReader());
            CellReaderFactory.RegisterGlyphReader(GlyphMode.Palette, new GlyphASCIIReader());

            CellReaderFactory.RegisterColorReader(ColorMode.None, new ColorVoidReader());
            CellReaderFactory.RegisterColorReader(ColorMode.FourBit, new ColorUInt4Reader());
            CellReaderFactory.RegisterColorReader(ColorMode.EightBit, new ColorUInt8Reader());
            CellReaderFactory.RegisterColorReader(ColorMode.Palette, new ColorUInt8Reader());

            CellReaderFactory.RegisterMetadataReader(MetadataMode.None, new MetadataVoidReader());
            CellReaderFactory.RegisterMetadataReader(MetadataMode.EightBit, new MetadataUInt8Reader());
            CellReaderFactory.RegisterMetadataReader(MetadataMode.SixteenBit, new MetadataUInt16Reader());

            CellWriterFactory = new CellWriterFactory();

            CellWriterFactory.RegisterGlyphWriter(GlyphMode.None, new GlyphVoidWriter());
            CellWriterFactory.RegisterGlyphWriter(GlyphMode.ASCII, new GlyphASCIIWriter());
            CellWriterFactory.RegisterGlyphWriter(GlyphMode.UTF16, new GlyphUnicodeWriter());
            CellWriterFactory.RegisterGlyphWriter(GlyphMode.Palette, new GlyphASCIIWriter());

            CellWriterFactory.RegisterColorWriter(ColorMode.None, new ColorVoidWriter());
            CellWriterFactory.RegisterColorWriter(ColorMode.FourBit, new ColorUInt4Writer());
            CellWriterFactory.RegisterColorWriter(ColorMode.EightBit, new ColorUInt8Writer());
            CellWriterFactory.RegisterColorWriter(ColorMode.Palette, new ColorUInt8Writer());

            CellWriterFactory.RegisterMetadataWriter(MetadataMode.None, new MetadataVoidWriter());
            CellWriterFactory.RegisterMetadataWriter(MetadataMode.EightBit, new MetadataVoidWriter());
            CellWriterFactory.RegisterMetadataWriter(MetadataMode.SixteenBit, new MetadataVoidWriter());
        }

        public NUIFile(NUIFileHeader header, Cell.CellData[,] cells)
        {
            Header = header;
            Payload = cells;
        }

        public static NUIFile FromStream(Stream stream)
        {
            var header = NUIFileHeader.FromStream(stream);
            var binaryReader = new BigEndianBinaryReader(stream);                
            var cellReader = CellReaderFactory.Build(header.GetCellConfig());

            Cell.CellData[,] cells = new Cell.CellData[header.Width, header.Height];
            for (ushort row = 0; row < header.Height; ++row)
                for (ushort col = 0; col < header.Width; ++col)
                    cells[col, row] = cellReader.Read(binaryReader);

            return new NUIFile(header, cells);
        }

        public void ToStream(Stream stream)
        {
            Header.ToStream(stream);
            var binaryWriter = new BigEndianBinaryWriter(stream);
            var cellWriter = CellWriterFactory.Build(CellConfig);

            for (ushort row = 0; row < Header.Height; ++row)
                for (ushort col = 0; col < Header.Width; ++col)
                    cellWriter.Write(binaryWriter, Payload[col, row]);
        }

        public static NUIFile FromFile(string path)
        {
            using (FileStream stream = File.OpenRead(path))
                return FromStream(stream);
        }

        public void ToFile(string path)
        {
            using (FileStream stream = File.OpenWrite(path))
                ToStream(stream);
        }
    }
}
