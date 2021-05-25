using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class ColorPairUInt4Reader : IColorPairReader
    {
        public ColorPair Read(BigEndianBinaryReader reader)
        {
            int b = reader.ReadByte();
            return new ColorPair((byte)(b >> 4), (byte)(b & 0xf));
        }
    }
}
