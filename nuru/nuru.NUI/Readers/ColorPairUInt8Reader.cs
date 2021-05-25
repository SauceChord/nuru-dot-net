using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class ColorPairUInt8Reader : IColorPairReader
    {
        public ColorPair Read(BigEndianBinaryReader reader)
        {
            byte background = reader.ReadByte();
            byte foreground = reader.ReadByte();
            return new ColorPair(foreground, background);
        }
    }
}
