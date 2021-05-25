using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class ColorPairUInt8Reader : IColorPairReader
    {
        public ColorPair Read(BigEndianBinaryReader reader)
        {
            byte background = reader.ReadByte(); // big endian low byte
            byte foreground = reader.ReadByte(); // big endian high byte
            return new ColorPair(foreground, background);
        }
    }
}
