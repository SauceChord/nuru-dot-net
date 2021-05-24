﻿using System.IO;

namespace nuru.NUI.Writers
{
    public class ColorPairUInt8Writer : WriterBase, IColorPairWriter
    {
        public ColorPairUInt8Writer(Stream stream) : base(stream)
        {
        }

        public void Write(ColorPair pair)
        {
            writer.Write(pair.Foreground); // high byte
            writer.Write(pair.Background); // low byte
        }
    }
}
