using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public class ColorPairUInt4Writer : IColorPairWriter
    {
        public void Write(BigEndianBinaryWriter writer, ColorPair pair)
        {
            writer.Write((byte)((pair.Foreground << 4) | (pair.Background & 0x0f)));
        }
    }
}
