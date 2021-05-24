using System;
using System.Collections.Generic;
using System.Text;

namespace nuru.NUI.Readers
{
    public class MetadataVoidReader : IMetadataReader
    {
        public ushort Read()
        {
            return default;
        }
    }
}
