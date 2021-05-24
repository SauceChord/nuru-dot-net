using System;
using System.Collections.Generic;
using System.Text;

namespace nuru.NUI.Readers
{
    public class CellReader
    {
        protected IGlyphReader glyphReader;
        protected IColorPairReader colorPairReader;
        protected IMetadataReader metadataReader;

        public CellReader(IGlyphReader glyphReader, IColorPairReader colorPairReader, IMetadataReader metadataReader)
        {
            this.glyphReader = glyphReader;
            this.colorPairReader = colorPairReader;
            this.metadataReader = metadataReader;
        }

        public Cell Read()
        {
            return new Cell(
                glyphReader.Read(), 
                colorPairReader.Read(), 
                metadataReader.Read());
        }
    }
}
