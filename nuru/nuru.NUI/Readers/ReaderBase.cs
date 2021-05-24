using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace nuru.NUI.Readers
{
    public abstract class ReaderBase
    {
        protected BinaryReader reader;

        public ReaderBase(Stream stream)
        {
            reader = new BinaryReader(stream, Encoding.BigEndianUnicode, true);
        }
    }
}
