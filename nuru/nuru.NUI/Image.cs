using System;
using System.Collections.Generic;
using System.Text;

namespace nuru.NUI
{
    public class Image
    {
        public ushort Width { get; protected internal set; }
        public ushort Height { get; protected internal set; }
        public GlyphMode GlyphMode { get; protected internal set; }
        public ColorMode ColorMode { get; protected internal set; }
        public MetadataMode MetadataMode { get; protected internal set; }
        public string GlyphPalette { get; protected internal set; }
        public string ColorPalette { get; protected internal set; }
        protected internal Cell[,] Cells { get; set; }

        public Cell GetCell(ushort column, ushort row)
        {
            if (column >= Width || row >= Height)
                throw new IndexOutOfRangeException("Attempting to access cell outside bounds.");

            return Cells[column, row];
        }
    }
}
