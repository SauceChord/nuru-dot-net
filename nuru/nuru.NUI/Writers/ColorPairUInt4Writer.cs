using System.IO;

namespace nuru.NUI.Writers
{
    public class ColorPairUInt4Writer : WriterBase, IColorPairWriter
    {
        public ColorPairUInt4Writer(Stream stream) : base(stream)
        {
        }

        public void Write(ColorPair pair)
        {
            writer.Write((byte)((pair.Foreground << 4) | (pair.Background & 0x0f)));
        }
    }
}
